using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBApi.Models;

namespace MongoDBApi.Services;

public class MongoDBService
{
    private readonly IMongoCollection<PhoneInfo> _phoneBookCollection;
    
    public MongoDBService(
        IOptions<MongoDBSettings> mongoDBSettings)
    {
        //데이터베이스 작업을 실행하기 위한 서버 인스턴스를 읽습니다. 이 클래스의 생성자에 MongoDB 연결 문자열이 제공됩니다.
        MongoClient Client = new MongoClient(
            mongoDBSettings.Value.ConnectionURL);

        IMongoDatabase database = Client.GetDatabase(
            mongoDBSettings.Value.DatabaseName);

        // GetCollection<TDocument>(collection) 메서드 호출에서 다음을 수행합니다.
        // collection은 컬렉션 이름을 나타냅니다.
        // TDocument는 컬렉션에 저장된 CLR 개체 형식을 나타냅니다.
        _phoneBookCollection = database.GetCollection<PhoneInfo>(
            mongoDBSettings.Value.CollectionName);
    }

    // DeleteOneAsync: 제공된 검색 조건과 일치하는 단일 문서를 삭제합니다.
    // FindTDocument>: 제공된 검색 조건과 일치하는 컬렉션의 모든 문서를 반환합니다.
    // InsertOneAsync: 제공된 개체를 컬렉션에 새 문서로 삽입합니다.
    // ReplaceOneAsync: 제공된 검색 조건과 일치하는 단일 문서를 제공된 개체로 바꿉니다.
    public async Task<List<PhoneInfo>> GetAsync() => 
        await _phoneBookCollection.Find(_ => true).ToListAsync();


    public async Task<PhoneInfo?> GetAsync(string id) =>
        await _phoneBookCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    // public async Task<PhoneInfo?> GetAsync(PhoneInfo phoneInfo) =>
    //     await _phoneBookCollection.Find(x => x.Id == phoneInfo.Id).FirstOrDefaultAsync();

    public async Task CreateAsync(PhoneInfo phoneInfo) =>
        await _phoneBookCollection.InsertOneAsync(phoneInfo);

    
    
    public async Task UpdateAsync(string id, PhoneInfo updatedPhoneInfo) =>
        await _phoneBookCollection.ReplaceOneAsync(x => x.Id == id, updatedPhoneInfo);

    // public async Task UpdateAsync(PhoneInfo updatedPhoneInfo) =>
    //     await _phoneBookCollection.ReplaceOneAsync(x => x.Id == updatedPhoneInfo.Id, updatedPhoneInfo);
    
    public async Task RemoveAsync(string id) =>
        await _phoneBookCollection.DeleteOneAsync(x => x.Id == id);

}