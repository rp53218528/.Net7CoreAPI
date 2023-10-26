using Learning.API.Models.Domain;

namespace Learning.API.Repositories
{
    public interface IImagerepository
    {
        Task<Image> upload(Image image);
    }
}
