using AEMAssessment.Models;

namespace AEMAssessment.Storage
{
    public class PlatformDataDbHelper
    {
        public async Task InsertData(IContextBuilder contextBuilder, List<Platform> dataList)
        {
            using (var dbContext = contextBuilder.GetContext())
            {
                dbContext.Platform.AddRange(dataList);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}