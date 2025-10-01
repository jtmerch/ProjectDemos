using NuveiPayBlazorDemo.Services.Interface;
using NuveiPayBlazorDemo.Shared;
using System;

namespace NuveiPayBlazorDemo.Services
{
    public class NuveiSQLMerchantData : INuveiSQLMerchantData
    {
        //private readonly AppDbContext _db;
        private readonly ILogger<NuveiSQLMerchantData> _logger;
        // private readonly ICacheService _cacheService;

        public NuveiSQLMerchantData(
           // AppDbContext db, 
            ILogger<NuveiSQLMerchantData> logger
           // ICacheService cacheService)
           )
        {
           // _db = db;
            _logger = logger;
           // _cacheService = cacheService;
        }

        public async Task<NuveiCC?> GetCCByUniqueUserIdAsync(string merchantID, string tblUserId)
        {
            NuveiCC getMerchantDb;

            //GET_CACHE if record is found get cache
          //  var cacheKey = $"{AppDefinitions.RedisKeyPrefix}{tblUserId}{merchantID}_sqlNuvei{AppDefinitions.MerchCCData}";
         //   var cacheData = _cacheService.GetCache<NuveiCC>(cacheKey);

            //_logger.LogInformation($"NuveiSQLMerchantData GetCCByUniqueUserIdAsync search cache data {cacheData}");
            //if (cacheData != null)
            //{
            //    _logger.LogInformation($"NuveiSQLMerchantData GetCCByUniqueUserIdAsync cache found {cacheData}");
            //    getMerchantDb = cacheData;
            //}
            //else
            //{

            //    getMerchantDb = await _db.NuveiCCMerchInfo.FirstOrDefaultAsync(x => x.merchantID == merchantID && x.Active == true
            //    && x.UserIdentifier == tblUserId &&
            //                  _db.GroupMembers.Any(g => g.Id == x.GroupMemberId && g.GroupActive == true));
            //    _logger.LogInformation($"NuveiSQLMerchantData GetCCByUniqueUserIdAsync method invoked.");

            //    //SET_CACHE if record is found set cache
            //    var expirationTime = DateTimeOffset.Now.AddMinutes(AppDefinitions.ExpirationMinutes);
            //    _cacheService.SetCache<NuveiCC>(cacheKey, getMerchantDb, expirationTime);

            //    _logger.LogInformation($"NuveiSQLMerchantData GetCCByUniqueUserIdAsync successful search setting cach merchsettings for user {getMerchantDb}, expires {expirationTime}");
            //    //END SET_CACHE

            //}


            var merchantModel = new NuveiCC();
            //if (getMerchantDb != null)
            //{

            //merchantModel.MerchantName = getMerchantDb.MerchantName;
            //merchantModel.merchantID = getMerchantDb.merchantID;
            //merchantModel.merchantSiteID = getMerchantDb.merchantSiteID;
            //merchantModel.merchantSecretKey = getMerchantDb.merchantSecretKey;
            //merchantModel.ProcessorType = getMerchantDb.ProcessorType;
            //merchantModel.UserIdentifier = getMerchantDb.UserIdentifier;
            //merchantModel.MerchantReturnURL = getMerchantDb.MerchantReturnURL;
            //merchantModel.Active = getMerchantDb.Active;
            //merchantModel.PerformMint = getMerchantDb.PerformMint;
            //merchantModel.GroupMemberId = getMerchantDb.GroupMemberId;
            //merchantModel.ISOId = getMerchantDb.ISOId;
            //merchantModel.MCCId = getMerchantDb.MCCId;

            merchantModel.MerchantName = "Test Blazor Merchant";
            merchantModel.merchantID = "3316684520456076214";
            merchantModel.merchantSiteID = "257108";
            merchantModel.merchantSecretKey = "key here";
            merchantModel.ProcessorType = "CC";
            merchantModel.UserIdentifier = Guid.NewGuid().ToString();
            merchantModel.MerchantReturnURL = "http://www.thompsonermchant.com/testreturn";
            merchantModel.Active = true;
            merchantModel.PerformMint = false;
            merchantModel.GroupMemberId = Guid.NewGuid();
            merchantModel.ISOId = Guid.NewGuid();
            merchantModel.MCCId = Guid.NewGuid();


            //  }


            _logger.LogInformation($"NuveiSQLMerchantData SQLMerchantData GetByIdAsync building merchant model - {merchantModel}.");


            return merchantModel;

            //return await _dbContext.MerchantModels.FirstOrDefaultAsync(x => x.id == id);
        }

    }
}
