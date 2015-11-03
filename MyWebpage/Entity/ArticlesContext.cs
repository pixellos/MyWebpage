using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyWebpage.Abstract;
using MyWebpage.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations;
using System.Threading;
using MyWebpage.Entity;

namespace MyWebpage.Entity
{
    using System.Security.Cryptography.X509Certificates;

    public class ArticlesContext : DbContext
    {
        private static string connectionString =
            @"Data Source=SQL5011.Smarterasp.net;Initial Catalog=DB_9D8EA4_towebpage;User Id=DB_9D8EA4_towebpage_admin;Password=DATA123456;";

        // "Server = tcp:lv9kyjkui8.database.windows.net, 1433; Database=MyWebpageDataBase;User ID = SQLDatabase@lv9kyjkui8;Password=Test123456; Trusted_Connection=False;Encrypt=True;Connection Timeout = 30;";
        public ArticlesContext() : base(connectionString)
        {
        }

        public DbSet<ArticleSheet> Articles { get; set; }
    }

    public sealed class DataBaseConnectionRepositoriesSingleton : IArticles, IProjects
    {
        private const int MilisecondsAtDay = 60*60*24*1000;
        private const string WakeingTheTaskMessange = "I'm Awake!";
        private Thread _databaseConnectionThread;

        public DataBaseConnectionRepositoriesSingleton()
        {
            _databaseConnectionThread = new Thread(BackgroundTasks) {IsBackground = true};
            KeepConnectionWithBase();
            _databaseConnectionThread.Start();
        }

        private void BackgroundTasks()
        {
            while (true)
            {
                KeepConnectionWithBase();
                try
                {
                    Thread.Sleep(MilisecondsAtDay);
                }
                catch (ThreadInterruptedException)
                {
                    Console.WriteLine(WakeingTheTaskMessange);
                }
            }
        }

        private void GetNewData()
        {
            _databaseConnectionThread.Interrupt();
        }

        private void KeepConnectionWithBase()
        {
            using (ArticlesContext connection = new ArticlesContext())
            {
                this.articles = connection.Articles.ToList<IArticleSheet>();
            }

            using (ProjectContext connection = new ProjectContext())
            {
                this.projectsList = (connection.Projects.ToList<IProject>());
            }
        }

        private readonly object articleLock = new object();
        private List<IArticleSheet> articles;

        public List<IArticleSheet> Articles
        {
            get { return this.articles; }
            set
            {
                lock (this.articleLock)
                {
                    ArticlesContext connection = new ArticlesContext();
                    this.articles = value;
                    foreach (var item in value)
                    {
                        connection.Articles.Add(item as ArticleSheet);
                    }
                    connection.SaveChanges();
                    connection.Dispose();
                }
                GetNewData();
            }
        }

        #region projects

        public void Add(IProject project)
        {
            if (project != null)
            {
                using (ProjectContext connection = new ProjectContext())
                {
                    connection.Projects.Add(project as Project);
                    try
                    {
                        connection.SaveChanges();
                    }
                    catch (Exception debugException)
                    {
                        Console.WriteLine(debugException);
                    }
                }
            }
            GetNewData();
        }

        public void RemoveById(string id)
        {
            using (ProjectContext connection = new ProjectContext())
            {
                var toremove = connection.Projects.SingleOrDefault(x => x.Id == id);
                connection.Projects.Remove(toremove);
                connection.SaveChanges();
            }
            GetNewData();
        }

        public void RemoveByName(string name)
        {
            using (ProjectContext connection = new ProjectContext())
            {
                var toremove = connection.Projects.SingleOrDefault(x => x.Name == name);
                if (toremove != null)
                {
                    connection.Projects.Remove(toremove);
                }
                connection.SaveChanges();
            }
            GetNewData();
        }

        private List<IProject> projectsList;

        public List<IProject> ProjectsList
        {
            get { return this.projectsList; }
            set { }
        }
        
    }
    #endregion
}

public class ArticleRepository : IArticles
{
    public List<IArticleSheet> Articles
    {
        get
        {
            ArticlesContext connection = new ArticlesContext();
            var toReturn = connection.Articles.ToList<IArticleSheet>();
            connection.Dispose();
            return toReturn;
        }
        set
        {
            if (value != null)
            {
                using (ArticlesContext connection = new ArticlesContext())
                {
                    foreach (var item in value)
                    {
                        connection.Articles.Add(item as ArticleSheet);
                    }
                    var info = new ArticleRepository().Articles;
                    connection.SaveChanges();
                }
            }
        }
    }
}