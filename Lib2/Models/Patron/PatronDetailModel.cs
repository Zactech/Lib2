﻿using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lib2.Models.Patron
{
    public class PatronDetailModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " "+ LastName;
        public string LibraryCardId { get; set; }
        public string Address { get; set; }
        public DateTime MemberSince { get; set; }
        public string Telephone { get; set; }
        public string HomeLibraryBranch
        { get; set; }
        public decimal  OverdueFees { get; set; }   
        public IEnumerable<Checkout1> AssetsCheckedout{get; set; }
        public IEnumerable<CheckoutHistory> CheckoutHistory{ get; set; }
        public IEnumerable<Hold> Holds { get; set; }
    }
}
