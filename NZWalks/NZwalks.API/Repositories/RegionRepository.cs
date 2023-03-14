using Microsoft.EntityFrameworkCore;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;
        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
           var region =await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(region == null)
            {
                return null; 
            }
            //Delete the region
            nZWalksDbContext.Regions.Remove(region);
           await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>>GetAllAsync()
        {
             return await nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
         return await nZWalksDbContext.Regions.FirstOrDefaultAsync(x =>x.Id == id);
            
        }

        public async Task<Region> UpdateAsync(Guid id,Region region)
        {
         var existingRegion=  await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
         
          if(existingRegion == null)
            {
                return null;
            }
            existingRegion.code = region.code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            nZWalksDbContext.SaveChangesAsync();
            return existingRegion;
        }

        public Task<Region> UpdateAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
