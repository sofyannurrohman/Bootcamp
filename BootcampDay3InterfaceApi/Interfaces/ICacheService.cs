namespace BootcampDay3InterfaceApi.Interface;
public interface ICacheService
{
    void Set(string key, object value);
    object? Get(string key);
}