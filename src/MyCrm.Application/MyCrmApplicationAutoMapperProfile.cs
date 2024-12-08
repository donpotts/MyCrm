using MyCrm.Customers;
using MyCrm.Addresses;
using MyCrm.ProductCategories;
using MyCrm.ServiceCategories;
using MyCrm.Contacts;
using MyCrm.Opportunities;
using MyCrm.Leads;
using MyCrm.Products;
using MyCrm.Services;
using MyCrm.Sales;
using MyCrm.Vendors;
using MyCrm.SupportCases;
using MyCrm.TodoTasks;
using MyCrm.Rewards;
using AutoMapper;

namespace MyCrm;

public class MyCrmApplicationAutoMapperProfile : Profile
{
    public MyCrmApplicationAutoMapperProfile()
    {
        CreateMap<Customer, CustomerDto>();
        CreateMap<CreateUpdateCustomerDto, Customer>();
        CreateMap<Address, AddressDto>();
        CreateMap<CreateUpdateAddressDto, Address>();
        CreateMap<ProductCategory, ProductCategoryDto>();
        CreateMap<CreateUpdateProductCategoryDto, ProductCategory>();
        CreateMap<ServiceCategory, ServiceCategoryDto>();
        CreateMap<CreateUpdateServiceCategoryDto, ServiceCategory>();
        CreateMap<Contact, ContactDto>();
        CreateMap<CreateUpdateContactDto, Contact>();
        CreateMap<Opportunity, OpportunityDto>();
        CreateMap<CreateUpdateOpportunityDto, Opportunity>();
        CreateMap<Lead, LeadDto>();
        CreateMap<CreateUpdateLeadDto, Lead>();
        CreateMap<Product, ProductDto>();
        CreateMap<CreateUpdateProductDto, Product>();
        CreateMap<Service, ServiceDto>();
        CreateMap<CreateUpdateServiceDto, Service>();
        CreateMap<Sale, SaleDto>();
        CreateMap<CreateUpdateSaleDto, Sale>();
        CreateMap<Vendor, VendorDto>();
        CreateMap<CreateUpdateVendorDto, Vendor>();
        CreateMap<SupportCase, SupportCaseDto>();
        CreateMap<CreateUpdateSupportCaseDto, SupportCase>();
        CreateMap<TodoTask, TodoTaskDto>();
        CreateMap<CreateUpdateTodoTaskDto, TodoTask>();
        CreateMap<Reward, RewardDto>();
        CreateMap<CreateUpdateRewardDto, Reward>();
        CreateMap<Address, MyCrm.Customers.AddressLookupDto>();
        CreateMap<Address, MyCrm.Contacts.AddressLookupDto>();
        CreateMap<Address, MyCrm.Leads.AddressLookupDto>();
        CreateMap<Address, MyCrm.Vendors.AddressLookupDto>();
        CreateMap<Contact, MyCrm.Leads.ContactLookupDto>();
        CreateMap<Service, MyCrm.Vendors.ServiceLookupDto>();
        CreateMap<Product, MyCrm.Vendors.ProductLookupDto>();
        CreateMap<ProductCategory, MyCrm.Products.ProductCategoryLookupDto>();
        CreateMap<ServiceCategory, MyCrm.Services.ServiceCategoryLookupDto>();
        CreateMap<Contact, MyCrm.Customers.ContactLookupDto>();
        CreateMap<Reward, MyCrm.Contacts.RewardLookupDto>();
        CreateMap<Opportunity, MyCrm.Leads.OpportunityLookupDto>();
    }
}
