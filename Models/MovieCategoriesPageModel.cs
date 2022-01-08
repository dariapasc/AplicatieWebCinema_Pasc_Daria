using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinema.Data;

namespace Cinema.Models
{
    public class MovieCategoriesPageModel:PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public void PopulateAssignedCategoryData(CinemaContext context, Movie movie)
        {
            var allCategories = context.Category;
            var movieCategories = new HashSet<int>(movie.MovieCategories.Select(c => c.CategoryID)); //
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryDataID = cat.CategoryID,
                    Name = cat.CategoryName,
                    Assigned = movieCategories.Contains(cat.CategoryID)
                });
            }
        }

        public void UpdateMovieCategories(CinemaContext context, string[] selectedCategories, Movie movieToUpdate)
        {
            if (selectedCategories == null)
            {
                movieToUpdate.MovieCategories = new List<MovieCategory>();
                return;
            }

            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var movieCategories = new HashSet<int>
            (movieToUpdate.MovieCategories.Select(c => c.Category.CategoryID));
            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.CategoryID.ToString()))
                {
                    if (!movieCategories.Contains(cat.CategoryID))
                    {
                        movieToUpdate.MovieCategories.Add(
                        new MovieCategory
                        {
                            MovieID = movieToUpdate.MovieID,
                            CategoryID = cat.CategoryID
                        });
                    }
                }
                else
                {
                    if (movieCategories.Contains(cat.CategoryID))
                    {
                        MovieCategory courseToRemove
                        = movieToUpdate
                        .MovieCategories
                       .SingleOrDefault(i => i.CategoryID == cat.CategoryID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
