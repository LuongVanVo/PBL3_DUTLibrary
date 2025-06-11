using Microsoft.Extensions.Options;
using PBL3_DUTLibrary.Data;
using PBL3_DUTLibrary.Helper;
using PBL3_DUTLibrary.Interfaces;
using MongoDB.Driver;

namespace PBL3_DUTLibrary.Services
{
	public class FeedbackService : IFeedbackService
	{
		private readonly IMongoCollection<FeedbackCustomers> _feedbacks;

		public FeedbackService(IOptions<MongoDbSettings> settings)
		{
			var client = new MongoClient(settings.Value.ConnectionString);
			var database = client.GetDatabase(settings.Value.DatabaseName);
			_feedbacks = database.GetCollection<FeedbackCustomers>(settings.Value.CollectionName);
		}
		public async Task<List<FeedbackCustomers>> GetAllFeedback()
		{
			return await _feedbacks
				.Find(_ => true)
				.SortByDescending(fb => fb.CreatedAt)  // Sắp xếp theo thời gian tạo giảm dần (mới nhất trước)
				.Limit(5)  // Giới hạn chỉ lấy 5 bản ghi
				.ToListAsync();
		}

		public async Task<FeedbackCustomers> GetFeedbackById(string id)
		{
			return await _feedbacks.Find(x => x.Id == id).FirstOrDefaultAsync();
		}

		public async Task<FeedbackCustomers> GetFeedbackByEmail(string email)
		{
			return await _feedbacks.Find(x => x.email == email).FirstOrDefaultAsync();
		}
		public async Task AddFeedback(FeedbackCustomers feedback)
		{
			await _feedbacks.InsertOneAsync(feedback);
		}
		public async Task<List<string>> GetFeedbackTexts()
		{
			var projection = Builders<FeedbackCustomers>.Projection.Include(x => x.feedback).Exclude("_id");
			var feedbacks = await _feedbacks.Find(_ => true).Project(projection).ToListAsync();
			return feedbacks.Select(fb => fb.GetValue("feedback").AsString).ToList();
		}
	}
}
