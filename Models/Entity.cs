using Newtonsoft.Json;

namespace PostgresTest.Models
{
  public abstract class Entity
  {
    public long Id { get; set; }

    public string ToJson()
    {
      return JsonConvert.SerializeObject(this);
    }
  }
}
