namespace DVDRental.Data.Services
{
    public class ActorServices: IActorServices
    {
        public Blog GetBlog(int blogId)
        {
            return appDbContext.Blogs
                .Include(blog => blog.Creator)
                .Include(blog => blog.Comments)
                    .ThenInclude(comment => comment.Author)
                .Include(blog => blog.Comments)
                    .ThenInclude(comment => comment.Comments)
                        .ThenInclude(reply => reply.Parent)
                .FirstOrDefault(blog => blog.Id == blogId);
        }
    }
}
