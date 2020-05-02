namespace MyMusic.Responses {
    public interface ResponseBuilder <T, K>{
        T BuildFrom(K domainObject);
    }
}