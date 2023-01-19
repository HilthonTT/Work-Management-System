﻿using UI.Library.Models;

namespace UI.Library.API
{
    public interface ICompanyEndpoint
    {
        Task<List<CompanyModel>> GetAll();
        Task<List<CompanyModel>> GetByName(string CompanyName);
        Task PostCompany(CompanyModel company);
        Task UpdateCompany(CompanyModel company);
    }
}