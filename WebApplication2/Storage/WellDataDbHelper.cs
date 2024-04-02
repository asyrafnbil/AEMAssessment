using AEMAssessment.Models;

namespace AEMAssessment.Storage
{
    public class WellDataDbHelper
    {
        public async Task InsertData(IContextBuilder contextBuilder, List<Well> dataList)
        {
            using (var dbContext = contextBuilder.GetContext())
            {
                dbContext.Well.AddRange(dataList);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}