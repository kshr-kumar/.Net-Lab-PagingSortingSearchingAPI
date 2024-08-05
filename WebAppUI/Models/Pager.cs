﻿namespace WebAppUI.Models
{
    public class Pager
    {
        public Pager()
        {
            
        }
        public string SearchText { get; set; } = ""; 
        public string Controller { get; set; } = "";
        public string Action { get; set; } = "Index"; 
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public int StartRecord { get; private set; }
        public int EndRecord { get; private set; }



        public Pager(int totalItems, int page, int pageSize = 10)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize); 
            int currentPage = page;
            int startPage = currentPage - 5;
            int endPage = currentPage + 4; 
            if (startPage <= 0) 
            {
                endPage = endPage - (startPage - 1); 
                startPage = 1; 
            }
            if (endPage > totalPages) 
            {
                endPage = totalPages; 
                if (endPage > 10) 
                {
                    startPage = endPage - 9;
                }
            }
            TotalItems = totalItems;
            CurrentPage = currentPage; 
            PageSize = pageSize; 
            TotalPages = totalPages; 
            StartPage = startPage;
            EndPage = endPage; 
            StartRecord = (CurrentPage - 1) * PageSize + 1; 
            EndRecord = StartRecord - 1 + PageSize;
        }
    }
}
