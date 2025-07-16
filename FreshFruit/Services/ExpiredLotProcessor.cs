using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FreshFruit.Models;

public class ExpiredLotProcessor : IHostedService, IDisposable
{
	private Timer _timer;
	private readonly IServiceScopeFactory _scopeFactory;

	public ExpiredLotProcessor(IServiceScopeFactory scopeFactory)
	{
		_scopeFactory = scopeFactory;
	}

	public Task StartAsync(CancellationToken cancellationToken)
	{
		_timer = new Timer(CheckExpiredLots, null, TimeSpan.Zero, TimeSpan.FromHours(6)); // kiểm tra mỗi 6 tiếng
		return Task.CompletedTask;
	}

	private void CheckExpiredLots(object? state)
	{
		using var scope = _scopeFactory.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<FreshFruitDbContext>();

		var today = DateOnly.FromDateTime(DateTime.Now);

		var expiredDetails = context.PurchaseReceiptDetails
			.Where(d => d.ExpiryDate != null && d.ExpiryDate <= today && d.Status != -1 && d.Quantity > 0)
			.ToList();

		foreach (var detail in expiredDetails)
		{
			var product = context.Products.FirstOrDefault(p => p.Id == detail.ProductId);
			if (product != null)
			{
				double quantityToReduce = detail.Quantity ?? 0;
				double currentQuantity = product.Quantity ?? 0;

				product.Quantity = Math.Max(0, currentQuantity - quantityToReduce);

				// Đánh dấu chi tiết này đã hết hạn và đã trừ
				detail.Status = -1;
			}
		}

		context.SaveChanges();
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		_timer?.Change(Timeout.Infinite, 0);
		return Task.CompletedTask;
	}

	public void Dispose()
	{
		_timer?.Dispose();
	}
}
