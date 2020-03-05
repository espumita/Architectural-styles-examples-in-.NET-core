namespace MyMusic.Responses {
    public interface ResponseMapper <T, K>{
        T From(K modelObject);
    }
}