namespace MyMusic.Shared {
    public interface ResponseBuilder <T, K>{
        T BuildFrom(K domainObject);
    }
}