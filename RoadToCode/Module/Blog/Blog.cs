using Nancy;
using Nancy.ModelBinding;
using RoadToCode.Module.Blog.DAL;

namespace RoadToCode.Module.Blog
{
    public class Blog : NancyModule
    {
        BlogRepository BlogRepository { get; set; }

        public Blog(BlogRepository repository) : base("/")
        {
            BlogRepository = repository;
            CrmSubmodule();
            GetPost();
        }

        
        public void GetPost()
        {
            Get["/Category/{category}"] = param =>
            {
                var model = BlogRepository.GetViewModel();
                model.ShowedPosts = BlogRepository.GetPostsFromCategory(param.category);
                return Negotiate.WithView("Overview").WithModel(model);
            };

            Get[@"/"] = param =>
            {
                var model = BlogRepository.GetViewModel();
                return Negotiate.WithView("Overview").WithModel(model);
            };
            Get["/{title}"] = param =>
            {
                string x = param.title;
                var model = BlogRepository.GetPostFromLink(x);
                return Negotiate.WithView("Post").WithModel(model);
            };

        }

        public void CrmSubmodule()
        {
            Get["Blog/Create"] = (param) =>
            {
                //

                return Negotiate.WithView("Edit").WithModel(DAL.Post.GetDefault());
                //
            };
            Post["Blog/Save"] = param =>
            {
                //
                Post x = this.Bind<Post>();
                BlogRepository.SavePost(x);
                return "Ok";
            };

            Get["/{title}/Edit"] = param =>
            {
                Post post = BlogRepository.GetPostFromLink(param.title);
                
                return Negotiate.WithView("Edit").WithModel(post);
            };

            Get["/{title}/Delete"] = param =>
            {
                BlogRepository.Delete(param.title);
                return "Deleted " + param.title;

            };

        }
    }
}