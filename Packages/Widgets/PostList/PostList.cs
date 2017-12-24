﻿using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Widgets
{
    [ViewComponent(Name = "PostList")]
    public class PostList : ViewComponent
    {
        IUnitOfWork _db;

        public PostList(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> Invoke(ListType listType, ContentType contentType = ContentType.Content, bool html = true, int length = 0, int count = 3)
        {
            IQueryable<BlogPost> query;
            switch (listType)
            {
                case ListType.FeaturedPosts:
                    query = _db.BlogPosts.All()
                        .Where(p => p.Published > DateTime.MinValue && p.IsFeatured)
                        .OrderByDescending(p => p.Published)
                        .Take(count);
                    break;
                case ListType.RecentPosts:
                    query = _db.BlogPosts.All()
                        .Where(p => p.Published > DateTime.MinValue)
                        .OrderByDescending(p => p.Published)
                        .Take(count);
                    break;
                case ListType.PopularPosts:
                    query = _db.BlogPosts.All()
                        .Where(p => p.Published > DateTime.MinValue)
                        .OrderByDescending(p => p.PostViews)
                        .Take(count);
                    break;
                default:
                    throw new ApplicationException("Unknown list type");
            }
            var posts = await query.ToListAsync();
            // apply filters
            foreach (var item in posts)
            {
                if(contentType == ContentType.Description)
                {
                    item.Content = item.Description;
                }
                if (!html)
                {
                    item.Content.StripHtml();
                }
                if(length > 0 && item.Content.Length > length)
                {
                    item.Content = item.Content.Substring(0, length) + "...";
                }
            }

            return View(query);
        }
    }

    public enum ListType
    {
        FeaturedPosts = 1,
        RecentPosts = 2,
        PopularPosts = 3
    }

    public enum ContentType
    {
        Content = 0,
        Description = 1
    }
}