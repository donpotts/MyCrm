using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCrm.ProductCategories;
using MyCrm.ServiceCategories;
using MyCrm.Rewards;
using MyCrm.Addresses;
using MyCrm.Products;
using MyCrm.Services;
using MyCrm.Opportunities;
using MyCrm.Contacts;
using MyCrm.Vendors;
using MyCrm.Leads;
using MyCrm.Customers;
using MyCrm.Sales;
using MyCrm.SupportCases;
using MyCrm.TodoTasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace MyCrm;

public class MyCrmDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<ProductCategory, int> _productCategoryRepository;
    private readonly IRepository<ServiceCategory, int> _serviceCategoryRepository;
    private readonly IRepository<Reward, int> _rewardRepository;
    private readonly IRepository<Address, int> _addressRepository;
    private readonly IRepository<Product, int> _productRepository;
    private readonly IRepository<Service, int> _serviceRepository;
    private readonly IRepository<Opportunity, int> _opportunityRepository;
    private readonly IRepository<Contact, int> _contactRepository;
    private readonly IRepository<Vendor, int> _vendorRepository;
    private readonly IRepository<Lead, int> _leadRepository;
    private readonly IRepository<Customer, int> _customerRepository;
    private readonly IRepository<Sale, int> _saleRepository;
    private readonly IRepository<SupportCase, int> _supportCaseRepository;
    private readonly IRepository<TodoTask, int> _todoTaskRepository;

    public MyCrmDataSeederContributor(
        IRepository<ProductCategory, int> productCategoryRepository,
        IRepository<ServiceCategory, int> serviceCategoryRepository,
        IRepository<Reward, int> rewardRepository,
        IRepository<Address, int> addressRepository,
        IRepository<Product, int> productRepository,
        IRepository<Service, int> serviceRepository,
        IRepository<Opportunity, int> opportunityRepository,
        IRepository<Contact, int> contactRepository,
        IRepository<Vendor, int> vendorRepository,
        IRepository<Lead, int> leadRepository,
        IRepository<Customer, int> customerRepository,
        IRepository<Sale, int> saleRepository,
        IRepository<SupportCase, int> supportCaseRepository,
        IRepository<TodoTask, int> todoTaskRepository)
    {
        _productCategoryRepository = productCategoryRepository;
        _serviceCategoryRepository = serviceCategoryRepository;
        _rewardRepository = rewardRepository;
        _addressRepository = addressRepository;
        _productRepository = productRepository;
        _serviceRepository = serviceRepository;
        _opportunityRepository = opportunityRepository;
        _contactRepository = contactRepository;
        _vendorRepository = vendorRepository;
        _leadRepository = leadRepository;
        _customerRepository = customerRepository;
        _saleRepository = saleRepository;
        _supportCaseRepository = supportCaseRepository;
        _todoTaskRepository = todoTaskRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        Dictionary<int, ProductCategory> productCategories = [];

        if (await _productCategoryRepository.GetCountAsync() <= 0)
        {
            productCategories[1] = await _productCategoryRepository.InsertAsync(
                new ProductCategory
                {
                    Name = "AI Software",
                    Description = "Software solutions with integrated AI capabilities",
                    Icon = "ai_software_icon.png",
                    TaxRate = "0.01%",
                    Notes = "",
                },
                autoSave: true
            );
            productCategories[2] = await _productCategoryRepository.InsertAsync(
                new ProductCategory
                {
                    Name = "Autonomous Drones",
                    Description = "Unmanned aerial vehicles with advanced AI navigation systems",
                    Icon = "autonomous_drones_icon.png",
                    TaxRate = "0.01%",
                    Notes = "",
                },
                autoSave: true
            );
            productCategories[3] = await _productCategoryRepository.InsertAsync(
                new ProductCategory
                {
                    Name = "AI Personal Assistants",
                    Description = "Virtual assistants powered by artificial intelligence for personal use",
                    Icon = "ai_personal_assistants_icon.png",
                    TaxRate = "0.01%",
                    Notes = "",
                },
                autoSave: true
            );
            productCategories[4] = await _productCategoryRepository.InsertAsync(
                new ProductCategory
                {
                    Name = "Smart Home Devices",
                    Description = "Home appliances and systems with AI for automation and control",
                    Icon = "smart_home_devices_icon.png",
                    TaxRate = "0.01%",
                    Notes = "",
                },
                autoSave: true
            );
            productCategories[5] = await _productCategoryRepository.InsertAsync(
                new ProductCategory
                {
                    Name = "AI Security Systems",
                    Description = "Advanced security systems using AI for threat detection and response",
                    Icon = "ai_security_systems_icon.png",
                    TaxRate = "0.01%",
                    Notes = "",
                },
                autoSave: true
            );
            productCategories[6] = await _productCategoryRepository.InsertAsync(
                new ProductCategory
                {
                    Name = "Healthcare AI",
                    Description = "AI applications for diagnostics, treatment planning, and patient management",
                    Icon = "healthcare_ai_icon.png",
                    TaxRate = "0.01%",
                    Notes = "",
                },
                autoSave: true
            );
            productCategories[7] = await _productCategoryRepository.InsertAsync(
                new ProductCategory
                {
                    Name = "AI Education Platforms",
                    Description = "Educational platforms utilizing AI for personalized learning experiences",
                    Icon = "ai_education_platforms_icon.png",
                    TaxRate = "0.01%",
                    Notes = "",
                },
                autoSave: true
            );
            productCategories[8] = await _productCategoryRepository.InsertAsync(
                new ProductCategory
                {
                    Name = "AI Financial Tools",
                    Description = "Financial tools and apps enhanced with AI for better decision making",
                    Icon = "ai_financial_tools_icon.png",
                    TaxRate = "0.01%",
                    Notes = "",
                },
                autoSave: true
            );
        }

        Dictionary<int, ServiceCategory> serviceCategories = [];

        if (await _serviceCategoryRepository.GetCountAsync() <= 0)
        {
            serviceCategories[1] = await _serviceCategoryRepository.InsertAsync(
                new ServiceCategory
                {
                    Name = "AI Consultancy",
                    Description = "Expert AI strategy and implementation services",
                    Icon = "ai_consultancy_icon.png",
                    TaxRate = "0.01%",
                    Notes = "",
                },
                autoSave: true
            );
            serviceCategories[2] = await _serviceCategoryRepository.InsertAsync(
                new ServiceCategory
                {
                    Name = "AI Data Analysis",
                    Description = "In-depth data analysis services using AI algorithms",
                    Icon = "ai_data_analysis_icon.png",
                    TaxRate = "0.01%",
                    Notes = "",
                },
                autoSave: true
            );
            serviceCategories[3] = await _serviceCategoryRepository.InsertAsync(
                new ServiceCategory
                {
                    Name = "AI Security Services",
                    Description = "Security services enhanced with AI for threat detection and response",
                    Icon = "ai_security_services_icon.png",
                    TaxRate = "0.01%",
                    Notes = "",
                },
                autoSave: true
            );
            serviceCategories[4] = await _serviceCategoryRepository.InsertAsync(
                new ServiceCategory
                {
                    Name = "AI Training Workshops",
                    Description = "Workshops and seminars to educate teams on AI applications",
                    Icon = "ai_training_workshops_icon.png",
                    TaxRate = "0.01%",
                    Notes = "",
                },
                autoSave: true
            );
            serviceCategories[5] = await _serviceCategoryRepository.InsertAsync(
                new ServiceCategory
                {
                    Name = "AI Integration Services",
                    Description = "Services to integrate AI into existing business systems",
                    Icon = "ai_integration_services_icon.png",
                    TaxRate = "0.01%",
                    Notes = "",
                },
                autoSave: true
            );
            serviceCategories[6] = await _serviceCategoryRepository.InsertAsync(
                new ServiceCategory
                {
                    Name = "AI Customer Support",
                    Description = "Customer support services powered by AI for efficiency and accuracy",
                    Icon = "ai_customer_support_icon.png",
                    TaxRate = "0.01%",
                    Notes = "",
                },
                autoSave: true
            );
            serviceCategories[7] = await _serviceCategoryRepository.InsertAsync(
                new ServiceCategory
                {
                    Name = "AI Research and Development",
                    Description = "R&D services for pioneering new AI technologies",
                    Icon = "ai_research_development_icon.png",
                    TaxRate = "0.01%",
                    Notes = "",
                },
                autoSave: true
            );
            serviceCategories[8] = await _serviceCategoryRepository.InsertAsync(
                new ServiceCategory
                {
                    Name = "AI Legal Advisory",
                    Description = "Legal services utilizing AI for contract analysis and legal research",
                    Icon = "ai_legal_advisory_icon.png",
                    TaxRate = "0.01%",
                    Notes = "",
                },
                autoSave: true
            );
        }

        Dictionary<int, Reward> rewards = [];

        if (await _rewardRepository.GetCountAsync() <= 0)
        {
            rewards[1] = await _rewardRepository.InsertAsync(
                new Reward
                {
                    Rewardpoints = 1200,
                    CreditsDollars = 1.2,
                    ConversionRate = "0.10",
                    ExpirationDate = new DateTime(2024, 12, 31, 0, 0, 0),
                    Notes = "",
                },
                autoSave: true
            );
            rewards[2] = await _rewardRepository.InsertAsync(
                new Reward
                {
                    Rewardpoints = 1500,
                    CreditsDollars = 1.5,
                    ConversionRate = "0.10",
                    ExpirationDate = new DateTime(2024, 12, 31, 0, 0, 0),
                    Notes = "",
                },
                autoSave: true
            );
            rewards[3] = await _rewardRepository.InsertAsync(
                new Reward
                {
                    Rewardpoints = 1800,
                    CreditsDollars = 1.8,
                    ConversionRate = "0.10",
                    ExpirationDate = new DateTime(2024, 12, 31, 0, 0, 0),
                    Notes = "",
                },
                autoSave: true
            );
        }

        Dictionary<int, Address> addresses = [];

        if (await _addressRepository.GetCountAsync() <= 0)
        {
            addresses[901] = await _addressRepository.InsertAsync(
                new Address
                {
                    Street = "100 Innovation Drive",
                    City = "Techville",
                    State = "TN",
                    ZipCode = 37153,
                    Country = "USA",
                    Photo = "cybernetics_corp_headquarters.jpg",
                    Notes = "",
                },
                autoSave: true
            );
            addresses[902] = await _addressRepository.InsertAsync(
                new Address
                {
                    Street = "500 Tech Park",
                    City = "Innovation City",
                    State = "TN",
                    ZipCode = 37153,
                    Country = "USA",
                    Photo = "tech_park_address.jpg",
                    Notes = "",
                },
                autoSave: true
            );
            addresses[903] = await _addressRepository.InsertAsync(
                new Address
                {
                    Street = "1234 Maple Street",
                    City = "Rockvale",
                    State = "TN",
                    ZipCode = 37153,
                    Country = "USA",
                    Photo = "maple_street_address.jpg",
                    Notes = "",
                },
                autoSave: true
            );
        }

        Dictionary<int, Product> products = [];

        if (await _productRepository.GetCountAsync() <= 0)
        {
            products[101] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "Quantum AI Processor",
                    Description = "A next-generation processor for advanced AI computations",
                    Price = 2499.99,
                    StockQuantity = 50,
                    Photo = "quantum_ai_processor.jpg",
                    Notes = "",
                    ProductCategory = productCategories[1],
                },
                autoSave: true
            );
            products[102] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "SkyNet Drone",
                    Description = "A high-endurance drone with real-time AI analytics capabilities",
                    Price = 4999.99,
                    StockQuantity = 20,
                    Photo = "skynet_drone.jpg",
                    Notes = "",
                    ProductCategory = productCategories[2],
                },
                autoSave: true
            );
            products[103] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "Home AI Butler",
                    Description = "An AI-powered device that manages your smart home ecosystem",
                    Price = 799.99,
                    StockQuantity = 100,
                    Photo = "home_ai_butler.jpg",
                    Notes = "",
                    ProductCategory = productCategories[3],
                },
                autoSave: true
            );
            products[104] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "AI Chipset Max",
                    Description = "Advanced chipset designed for deep learning operations",
                    Price = 1299.99,
                    StockQuantity = 75,
                    Photo = "ai_chipset_max.jpg",
                    Notes = "",
                    ProductCategory = productCategories[1],
                },
                autoSave: true
            );
            products[105] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "Eagle Eye Drone",
                    Description = "Drone with enhanced vision AI for precision surveillance",
                    Price = 3500,
                    StockQuantity = 30,
                    Photo = "eagle_eye_drone.jpg",
                    Notes = "",
                    ProductCategory = productCategories[2],
                },
                autoSave: true
            );
            products[106] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "AI Coffee Maker",
                    Description = "Smart coffee maker with personalized AI taste calibration",
                    Price = 299.99,
                    StockQuantity = 200,
                    Photo = "ai_coffee_maker.jpg",
                    Notes = "",
                    ProductCategory = productCategories[3],
                },
                autoSave: true
            );
            products[107] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "Neural Net Core",
                    Description = "Processor that mimics human brain neural networks",
                    Price = 3199.99,
                    StockQuantity = 40,
                    Photo = "neural_net_core.jpg",
                    Notes = "",
                    ProductCategory = productCategories[1],
                },
                autoSave: true
            );
            products[108] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "Aqua Scout Drone",
                    Description = "Water-resistant drone with AI for marine research",
                    Price = 2800,
                    StockQuantity = 15,
                    Photo = "aqua_scout_drone.jpg",
                    Notes = "",
                    ProductCategory = productCategories[2],
                },
                autoSave: true
            );
            products[109] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "AI Fitness Coach",
                    Description = "Wearable device with AI for personalized fitness coaching",
                    Price = 499.99,
                    StockQuantity = 150,
                    Photo = "ai_fitness_coach.jpg",
                    Notes = "",
                    ProductCategory = productCategories[3],
                },
                autoSave: true
            );
            products[110] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "Quantum Data Server",
                    Description = "High-speed server optimized for quantum AI calculations",
                    Price = 10000,
                    StockQuantity = 10,
                    Photo = "quantum_data_server.jpg",
                    Notes = "",
                    ProductCategory = productCategories[1],
                },
                autoSave: true
            );
            products[111] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "Strato Climber Drone",
                    Description = "High-altitude drone with AI for atmospheric studies",
                    Price = 4200,
                    StockQuantity = 25,
                    Photo = "strato_climber_drone.jpg",
                    Notes = "",
                    ProductCategory = productCategories[2],
                },
                autoSave: true
            );
            products[112] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "AI Smart Lock",
                    Description = "Home security lock with facial recognition AI technology",
                    Price = 249.99,
                    StockQuantity = 300,
                    Photo = "ai_smart_lock.jpg",
                    Notes = "",
                    ProductCategory = productCategories[3],
                },
                autoSave: true
            );
            products[113] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "AI Dev Kit",
                    Description = "Development kit with AI modules for tech enthusiasts",
                    Price = 599.99,
                    StockQuantity = 120,
                    Photo = "ai_dev_kit.jpg",
                    Notes = "",
                    ProductCategory = productCategories[1],
                },
                autoSave: true
            );
            products[114] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "Night Hawk Drone",
                    Description = "Stealth drone with AI for nocturnal monitoring",
                    Price = 3800,
                    StockQuantity = 18,
                    Photo = "night_hawk_drone.jpg",
                    Notes = "",
                    ProductCategory = productCategories[2],
                },
                autoSave: true
            );
            products[115] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "AI Pet Companion",
                    Description = "AI device that interacts and cares for your pets",
                    Price = 349.99,
                    StockQuantity = 80,
                    Photo = "ai_pet_companion.jpg",
                    Notes = "",
                    ProductCategory = productCategories[3],
                },
                autoSave: true
            );
            products[116] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "AI Supercomputer",
                    Description = "Supercomputer with AI for complex problem solving",
                    Price = 15000,
                    StockQuantity = 5,
                    Photo = "ai_supercomputer.jpg",
                    Notes = "",
                    ProductCategory = productCategories[1],
                },
                autoSave: true
            );
            products[117] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "Recon Scout Drone",
                    Description = "Compact drone with AI for tactical reconnaissance",
                    Price = 3100,
                    StockQuantity = 22,
                    Photo = "recon_scout_drone.jpg",
                    Notes = "",
                    ProductCategory = productCategories[2],
                },
                autoSave: true
            );
            products[118] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "AI Thermostat",
                    Description = "Intelligent thermostat with AI for energy optimization",
                    Price = 199.99,
                    StockQuantity = 250,
                    Photo = "ai_thermostat.jpg",
                    Notes = "",
                    ProductCategory = productCategories[3],
                },
                autoSave: true
            );
            products[119] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "AI Crypto Miner",
                    Description = "Dedicated AI system for efficient cryptocurrency mining",
                    Price = 4500,
                    StockQuantity = 60,
                    Photo = "ai_crypto_miner.jpg",
                    Notes = "",
                    ProductCategory = productCategories[1],
                },
                autoSave: true
            );
            products[120] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "Forest Ranger Drone",
                    Description = "AI drone for monitoring wildlife and forest health",
                    Price = 3600,
                    StockQuantity = 28,
                    Photo = "forest_ranger_drone.jpg",
                    Notes = "",
                    ProductCategory = productCategories[2],
                },
                autoSave: true
            );
            products[121] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "AI Language Tutor",
                    Description = "AI-powered device for learning new languages",
                    Price = 399.99,
                    StockQuantity = 90,
                    Photo = "ai_language_tutor.jpg",
                    Notes = "",
                    ProductCategory = productCategories[3],
                },
                autoSave: true
            );
            products[122] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "AI Genome Analyzer",
                    Description = "AI system for rapid genome sequencing and analysis",
                    Price = 7500,
                    StockQuantity = 35,
                    Photo = "ai_genome_analyzer.jpg",
                    Notes = "",
                    ProductCategory = productCategories[1],
                },
                autoSave: true
            );
            products[123] = await _productRepository.InsertAsync(
                new Product
                {
                    Name = "Urban Explorer Drone",
                    Description = "Drone with AI for urban mapping and exploration",
                    Price = 3300,
                    StockQuantity = 12,
                    Photo = "urban_explorer_drone.jpg",
                    Notes = "",
                    ProductCategory = productCategories[2],
                },
                autoSave: true
            );
        }

        Dictionary<int, Service> services = [];

        if (await _serviceRepository.GetCountAsync() <= 0)
        {
            services[201] = await _serviceRepository.InsertAsync(
                new Service
                {
                    Name = "AI Predictive Maintenance",
                    Description = "AI-driven service to predict and prevent equipment failures",
                    Recurring = "Yes",
                    Icon = "ai_predictive_maintenance_icon.png",
                    Notes = "",
                    ServiceCategory = serviceCategories[1],
                },
                autoSave: true
            );
            services[202] = await _serviceRepository.InsertAsync(
                new Service
                {
                    Name = "AI Market Forecasting",
                    Description = "Predictive market analysis service using AI to forecast trends",
                    Recurring = "Yes",
                    Icon = "ai_market_forecasting_icon.png",
                    Notes = "",
                    ServiceCategory = serviceCategories[2],
                },
                autoSave: true
            );
            services[203] = await _serviceRepository.InsertAsync(
                new Service
                {
                    Name = "AI Cybersecurity Monitoring",
                    Description = "Continuous monitoring and threat detection using AI",
                    Recurring = "Yes",
                    Icon = "ai_cybersecurity_monitoring_icon.png",
                    Notes = "",
                    ServiceCategory = serviceCategories[3],
                },
                autoSave: true
            );
            services[204] = await _serviceRepository.InsertAsync(
                new Service
                {
                    Name = "AI Business Optimization",
                    Description = "AI service to enhance business efficiency and productivity",
                    Recurring = "Yes",
                    Icon = "ai_business_optimization_icon.png",
                    Notes = "",
                    ServiceCategory = serviceCategories[1],
                },
                autoSave: true
            );
            services[205] = await _serviceRepository.InsertAsync(
                new Service
                {
                    Name = "AI Personalized Marketing",
                    Description = "Targeted marketing service using AI for customer engagement",
                    Recurring = "Yes",
                    Icon = "ai_personalized_marketing_icon.png",
                    Notes = "",
                    ServiceCategory = serviceCategories[2],
                },
                autoSave: true
            );
            services[206] = await _serviceRepository.InsertAsync(
                new Service
                {
                    Name = "AI Fraud Detection",
                    Description = "AI service for real-time financial fraud detection",
                    Recurring = "Yes",
                    Icon = "ai_fraud_detection_icon.png",
                    Notes = "",
                    ServiceCategory = serviceCategories[3],
                },
                autoSave: true
            );
            services[207] = await _serviceRepository.InsertAsync(
                new Service
                {
                    Name = "AI Talent Acquisition",
                    Description = "Automated recruitment service using AI to find the best candidates",
                    Recurring = "Yes",
                    Icon = "ai_talent_acquisition_icon.png",
                    Notes = "",
                    ServiceCategory = serviceCategories[1],
                },
                autoSave: true
            );
            services[208] = await _serviceRepository.InsertAsync(
                new Service
                {
                    Name = "AI Content Creation",
                    Description = "Content generation service using AI for various media formats",
                    Recurring = "Yes",
                    Icon = "ai_content_creation_icon.png",
                    Notes = "",
                    ServiceCategory = serviceCategories[2],
                },
                autoSave: true
            );
        }

        Dictionary<int, Opportunity> opportunities = [];

        if (await _opportunityRepository.GetCountAsync() <= 0)
        {
            opportunities[601] = await _opportunityRepository.InsertAsync(
                new Opportunity
                {
                    EstimatedCloseDate = new DateTime(2024, 12, 31, 0, 0, 0),
                    Stage = "Negotiation",
                    Icon = "opportunity_stage_icon.png",
                    Notes = "",
                },
                autoSave: true
            );
            opportunities[602] = await _opportunityRepository.InsertAsync(
                new Opportunity
                {
                    EstimatedCloseDate = new DateTime(2024, 11, 30, 0, 0, 0),
                    Stage = "Proposal",
                    Icon = "opportunity_proposal_icon.png",
                    Notes = "",
                },
                autoSave: true
            );
            opportunities[603] = await _opportunityRepository.InsertAsync(
                new Opportunity
                {
                    EstimatedCloseDate = new DateTime(2024, 10, 15, 0, 0, 0),
                    Stage = "Qualification",
                    Icon = "opportunity_qualification_icon.png",
                    Notes = "",
                },
                autoSave: true
            );
            opportunities[604] = await _opportunityRepository.InsertAsync(
                new Opportunity
                {
                    EstimatedCloseDate = new DateTime(2024, 9, 20, 0, 0, 0),
                    Stage = "Presentation",
                    Icon = "opportunity_presentation_icon.png",
                    Notes = "",
                },
                autoSave: true
            );
            opportunities[605] = await _opportunityRepository.InsertAsync(
                new Opportunity
                {
                    EstimatedCloseDate = new DateTime(2024, 8, 25, 0, 0, 0),
                    Stage = "Discovery",
                    Icon = "opportunity_discovery_icon.png",
                    Notes = "",
                },
                autoSave: true
            );
            opportunities[606] = await _opportunityRepository.InsertAsync(
                new Opportunity
                {
                    EstimatedCloseDate = new DateTime(2024, 7, 30, 0, 0, 0),
                    Stage = "Analysis",
                    Icon = "opportunity_analysis_icon.png",
                    Notes = "",
                },
                autoSave: true
            );
            opportunities[607] = await _opportunityRepository.InsertAsync(
                new Opportunity
                {
                    EstimatedCloseDate = new DateTime(2024, 6, 15, 0, 0, 0),
                    Stage = "Value Proposition",
                    Icon = "opportunity_value_proposition_icon.png",
                    Notes = "",
                },
                autoSave: true
            );
            opportunities[608] = await _opportunityRepository.InsertAsync(
                new Opportunity
                {
                    EstimatedCloseDate = new DateTime(2024, 5, 10, 0, 0, 0),
                    Stage = "Decision Making",
                    Icon = "opportunity_decision_making_icon.png",
                    Notes = "",
                },
                autoSave: true
            );
        }

        Dictionary<int, Contact> contacts = [];

        if (await _contactRepository.GetCountAsync() <= 0)
        {
            contacts[801] = await _contactRepository.InsertAsync(
                new Contact
                {
                    Name = "Alex Taylor",
                    Email = "a.taylor@corporate.com",
                    Phone = 1234567890,
                    Role = "Chief Technology Officer",
                    Photo = "profile1.jpg",
                    Notes = "",
                    Address = addresses[901],
                    Reward = rewards[1],
                },
                autoSave: true
            );
            contacts[802] = await _contactRepository.InsertAsync(
                new Contact
                {
                    Name = "Jordan Lee",
                    Email = "j.lee@techcompany.com",
                    Phone = 1234567892,
                    Role = "Procurement Manager",
                    Photo = "profile2.jpg",
                    Notes = "",
                    Address = addresses[902],
                    Reward = rewards[2],
                },
                autoSave: true
            );
            contacts[803] = await _contactRepository.InsertAsync(
                new Contact
                {
                    Name = "Casey Morgan",
                    Email = "c.morgan@independent.com",
                    Phone = 1234567893,
                    Role = "Independent Researcher",
                    Photo = "profile3.jpg",
                    Notes = "",
                    Address = addresses[903],
                    Reward = rewards[3],
                },
                autoSave: true
            );
        }

        Dictionary<int, Vendor> vendors = [];

        if (await _vendorRepository.GetCountAsync() <= 0)
        {
            vendors[501] = await _vendorRepository.InsertAsync(
                new Vendor
                {
                    Name = "AI Components Ltd.",
                    ContactName = "John Smith",
                    Phone = 1234567891,
                    Email = "contact@aicomp.com",
                    Logo = "ai_components_ltd_logo.png",
                    Notes = "",
                    Address = addresses[901],
                    Service = services[201],
                    Product = products[101],
                },
                autoSave: true
            );
            vendors[502] = await _vendorRepository.InsertAsync(
                new Vendor
                {
                    Name = "DroneTech Innovations",
                    ContactName = "Sarah Connor",
                    Phone = 1234567894,
                    Email = "s.connor@dronetech.com",
                    Logo = "dronetech_innovations_logo.png",
                    Notes = "",
                    Address = addresses[902],
                    Service = services[202],
                    Product = products[102],
                },
                autoSave: true
            );
            vendors[503] = await _vendorRepository.InsertAsync(
                new Vendor
                {
                    Name = "HomeTech Solutions",
                    ContactName = "Alan Turing",
                    Phone = 1234567895,
                    Email = "alan.turing@hometechsolutions.com",
                    Logo = "hometech_solutions_logo.png",
                    Notes = "",
                    Address = addresses[903],
                    Service = services[203],
                    Product = products[103],
                },
                autoSave: true
            );
        }

        Dictionary<int, Lead> leads = [];

        if (await _leadRepository.GetCountAsync() <= 0)
        {
            leads[401] = await _leadRepository.InsertAsync(
                new Lead
                {
                    Source = "Online Webinar",
                    Status = "Interested",
                    PotentialValue = 15000,
                    Photo = "lead_profile_pic.jpg",
                    Notes = "",
                    Address = addresses[901],
                    Contact = contacts[801],
                    Opportunity = opportunities[601],
                },
                autoSave: true
            );
            leads[402] = await _leadRepository.InsertAsync(
                new Lead
                {
                    Source = "Tech Expo",
                    Status = "Evaluating",
                    PotentialValue = 25000,
                    Photo = "tech_expo_booth.jpg",
                    Notes = "",
                    Address = addresses[902],
                    Contact = contacts[802],
                    Opportunity = opportunities[602],
                },
                autoSave: true
            );
            leads[403] = await _leadRepository.InsertAsync(
                new Lead
                {
                    Source = "Social Media",
                    Status = "Contacted",
                    PotentialValue = 5000,
                    Photo = "social_media_ad.jpg",
                    Notes = "",
                    Address = addresses[903],
                    Contact = contacts[803],
                    Opportunity = opportunities[603],
                },
                autoSave: true
            );
        }

        Dictionary<int, Customer> customers = [];

        if (await _customerRepository.GetCountAsync() <= 0)
        {
            customers[301] = await _customerRepository.InsertAsync(
                new Customer
                {
                    Name = "Cybernetics Corp",
                    Type = "B2B",
                    Industry = "Robotics",
                    Logo = "cybernetics_corp_logo.jpg",
                    Notes = "",
                    Address = addresses[901],
                    Contact = contacts[801],
                },
                autoSave: true
            );
            customers[302] = await _customerRepository.InsertAsync(
                new Customer
                {
                    Name = "RoboTech Inc.",
                    Type = "B2B",
                    Industry = "Manufacturing",
                    Logo = "robotech_inc_logo.jpg",
                    Notes = "",
                    Address = addresses[902],
                    Contact = contacts[802],
                },
                autoSave: true
            );
            customers[303] = await _customerRepository.InsertAsync(
                new Customer
                {
                    Name = "Jane Engineering",
                    Type = "B2C",
                    Industry = "N/A",
                    Logo = "jane_eng_profile.jpg",
                    Notes = "",
                    Address = addresses[903],
                    Contact = contacts[803],
                },
                autoSave: true
            );
        }

        Dictionary<int, Sale> sales = [];

        if (await _saleRepository.GetCountAsync() <= 0)
        {
            sales[701] = await _saleRepository.InsertAsync(
                new Sale
                {
                    ProductId = "101",
                    ServiceId = "201",
                    CustomerId = 301,
                    Quantity = 10,
                    TotalAmount = 24999.9,
                    SaleDate = new DateTime(2024, 6, 1, 0, 0, 0),
                    ReceiptPhoto = "sale_receipt.jpg",
                    Notes = "",
                },
                autoSave: true
            );
            sales[702] = await _saleRepository.InsertAsync(
                new Sale
                {
                    ProductId = "102",
                    ServiceId = "NULL",
                    CustomerId = 302,
                    Quantity = 5,
                    TotalAmount = 24999.95,
                    SaleDate = new DateTime(2024, 6, 15, 0, 0, 0),
                    ReceiptPhoto = "sale_receipt_702.jpg",
                    Notes = "",
                },
                autoSave: true
            );
            sales[703] = await _saleRepository.InsertAsync(
                new Sale
                {
                    ProductId = "NULL",
                    ServiceId = "202",
                    CustomerId = 303,
                    Quantity = 1,
                    TotalAmount = 199.99,
                    SaleDate = new DateTime(2024, 6, 20, 0, 0, 0),
                    ReceiptPhoto = "sale_receipt_703.jpg",
                    Notes = "",
                },
                autoSave: true
            );
            sales[704] = await _saleRepository.InsertAsync(
                new Sale
                {
                    ProductId = "103",
                    ServiceId = "NULL",
                    CustomerId = 304,
                    Quantity = 3,
                    TotalAmount = 2399.97,
                    SaleDate = new DateTime(2024, 7, 5, 0, 0, 0),
                    ReceiptPhoto = "sale_receipt_704.jpg",
                    Notes = "",
                },
                autoSave: true
            );
            sales[705] = await _saleRepository.InsertAsync(
                new Sale
                {
                    ProductId = "104",
                    ServiceId = "204",
                    CustomerId = 305,
                    Quantity = 7,
                    TotalAmount = 9099.93,
                    SaleDate = new DateTime(2024, 7, 12, 0, 0, 0),
                    ReceiptPhoto = "sale_receipt_705.jpg",
                    Notes = "",
                },
                autoSave: true
            );
            sales[706] = await _saleRepository.InsertAsync(
                new Sale
                {
                    ProductId = "105",
                    ServiceId = "NULL",
                    CustomerId = 306,
                    Quantity = 2,
                    TotalAmount = 6999.98,
                    SaleDate = new DateTime(2024, 7, 19, 0, 0, 0),
                    ReceiptPhoto = "sale_receipt_706.jpg",
                    Notes = "",
                },
                autoSave: true
            );
            sales[707] = await _saleRepository.InsertAsync(
                new Sale
                {
                    ProductId = "106",
                    ServiceId = "205",
                    CustomerId = 307,
                    Quantity = 4,
                    TotalAmount = 1199.96,
                    SaleDate = new DateTime(2024, 7, 26, 0, 0, 0),
                    ReceiptPhoto = "sale_receipt_707.jpg",
                    Notes = "",
                },
                autoSave: true
            );
            sales[708] = await _saleRepository.InsertAsync(
                new Sale
                {
                    ProductId = "107",
                    ServiceId = "NULL",
                    CustomerId = 308,
                    Quantity = 1,
                    TotalAmount = 10000,
                    SaleDate = new DateTime(2024, 8, 2, 0, 0, 0),
                    ReceiptPhoto = "sale_receipt_708.jpg",
                    Notes = "",
                },
                autoSave: true
            );
        }

        Dictionary<int, SupportCase> supportCases = [];

        if (await _supportCaseRepository.GetCountAsync() <= 0)
        {
            supportCases[1] = await _supportCaseRepository.InsertAsync(
                new SupportCase
                {
                    CustomerId = 301,
                    ProductId = 101,
                    ServiceId = "0",
                    Status = "Open",
                    Description = "Issue with Widget A",
                    CreatedDateTime = new DateTime(2024, 1, 31, 0, 0, 0),
                    ModifiedDateTime = new DateTime(2024, 2, 1, 0, 0, 0),
                    UserId = 1,
                    FollowupDate = "2024-02-31",
                    Icon = "",
                    Notes = "",
                },
                autoSave: true
            );
            supportCases[2] = await _supportCaseRepository.InsertAsync(
                new SupportCase
                {
                    CustomerId = 302,
                    ProductId = 102,
                    ServiceId = "0",
                    Status = "Open",
                    Description = "Issue with Gadget B",
                    CreatedDateTime = new DateTime(2024, 2, 1, 0, 0, 0),
                    ModifiedDateTime = new DateTime(2024, 2, 2, 0, 0, 0),
                    UserId = 2,
                    FollowupDate = "3/2/2024",
                    Icon = "",
                    Notes = "",
                },
                autoSave: true
            );
            supportCases[3] = await _supportCaseRepository.InsertAsync(
                new SupportCase
                {
                    CustomerId = 303,
                    ProductId = 103,
                    ServiceId = "0",
                    Status = "Open",
                    Description = "Issue with Device C",
                    CreatedDateTime = new DateTime(2024, 2, 3, 0, 0, 0),
                    ModifiedDateTime = new DateTime(2024, 2, 4, 0, 0, 0),
                    UserId = 3,
                    FollowupDate = "3/3/2024",
                    Icon = "",
                    Notes = "",
                },
                autoSave: true
            );
        }

        Dictionary<int, TodoTask> todoTasks = [];

        if (await _todoTaskRepository.GetCountAsync() <= 0)
        {
            todoTasks[1] = await _todoTaskRepository.InsertAsync(
                new TodoTask
                {
                    Name = "Follow up with lead",
                    Description = "Call John Doe to discuss Widget A",
                    AssignedTo = 1,
                    Status = "Open",
                    DueDate = new DateTime(2024, 12, 31, 0, 0, 0),
                    CreatedDateTime = new DateTime(2024, 1, 27, 0, 0, 0),
                    ModifiedDateTime = new DateTime(2024, 1, 28, 0, 0, 0),
                    UserId = 1,
                    FollowupDate = new DateTime(2024, 2, 27, 0, 0, 0),
                    Icon = "",
                    Notes = "",
                },
                autoSave: true
            );
            todoTasks[2] = await _todoTaskRepository.InsertAsync(
                new TodoTask
                {
                    Name = "Send proposal",
                    Description = "Email proposal for Gadget B to Jane Smith",
                    AssignedTo = 2,
                    Status = "In Progress",
                    DueDate = new DateTime(2024, 12, 31, 0, 0, 0),
                    CreatedDateTime = new DateTime(2024, 1, 29, 0, 0, 0),
                    ModifiedDateTime = new DateTime(2024, 1, 30, 0, 0, 0),
                    UserId = 2,
                    FollowupDate = new DateTime(2024, 2, 29, 0, 0, 0),
                    Icon = "",
                    Notes = "",
                },
                autoSave: true
            );
            todoTasks[3] = await _todoTaskRepository.InsertAsync(
                new TodoTask
                {
                    Name = "Check in with customer",
                    Description = "Call Jim Brown to discuss Device C",
                    AssignedTo = 3,
                    Status = "Open",
                    DueDate = new DateTime(2024, 12, 31, 0, 0, 0),
                    CreatedDateTime = new DateTime(2024, 1, 31, 0, 0, 0),
                    ModifiedDateTime = new DateTime(2024, 2, 1, 0, 0, 0),
                    UserId = 3,
                    FollowupDate = new DateTime(2024, 3, 1, 0, 0, 0),
                    Icon = "",
                    Notes = "",
                },
                autoSave: true
            );
        }
    }
}
