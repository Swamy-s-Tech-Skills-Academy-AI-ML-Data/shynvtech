# ShyvnTech - Tech Innovation Collective 🚀

[![.NET 9.0](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![Blazor Server](https://img.shields.io/badge/Blazor-Server-512BD4?style=flat&logo=blazor)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![Azure](https://img.shields.io/badge/Deploy-Azure-0078D4?style=flat&logo=microsoft-azure)](https://azure.microsoft.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

A collaborative platform designed for **students**, **professionals**, and **tech enthusiasts** to connect, learn, and innovate together. Our mission is to empower the future of technology through community-driven innovation and continuous learning.

## 🎯 What is ShyvnTech?

ShyvnTech is a **Tech Innovation Collective** that serves as a hub for:

- **🧑‍🎓 Students** seeking hands-on experience and industry connections
- **👩‍💼 Professionals** looking to stay current with emerging technologies
- **🚀 Innovators** passionate about solving real-world problems through technology
- **🌍 Global Community** committed to ethical and impactful tech development

### 🌟 Core Focus Areas

- **🤖 AI & Generative AI** - Exploring cutting-edge advancements in artificial intelligence
- **🔓 Open Source** - Contributing to shared projects and community-driven solutions
- **🛡️ Cybersecurity & Ethics** - Fostering responsible AI and secure development practices
- **📈 Career Growth** - Helping members advance in their technology careers
- **❤️ Tech for Social Good** - Driving impactful projects that create positive change
- **🧪 Hands-on Labs** - Practical learning through interactive sessions and real-world projects

## 🏗️ Project Structure

```text
ShyvnTech/
├── 📁 docs/                          # Documentation
│   ├── 📄 api-documentation.md       # API reference and guides
│   ├── 📄 architecture.md            # System architecture overview
│   ├── 📄 component-architecture.md  # Modern component architecture guide
│   ├── 📄 deployment-guide.md        # Deployment instructions
│   ├── 📄 development-guide.md       # Development setup guide
│   ├── 📄 getting-started.md         # Quick start guide
│   ├── 📄 project-structure.md       # Detailed project structure
│   ├── 📄 pdf-download.md           # PDF delivery implementation guide
│   └── 📁 reviews/                   # Code reviews and assessments
│
├── 📁 src/                           # Source code
│   ├── 🌐 ShyvnTech.Web/            # Modern Blazor web application
│   │   ├── 📁 Components/            # Modular Blazor components
│   │   │   ├── 📁 Layout/           # Layout components (NavMenu, MainLayout, Scripts)
│   │   │   ├── 📁 Home/             # Home page components (Hero, Carousel, FeatureGrid)
│   │   │   ├── 📁 Shared/           # Reusable components (GradientButton)
│   │   │   └── 📁 Pages/            # Page components (Home, Magazines, etc.)
│   │   ├── 📁 Styles/               # Tailwind CSS source files
│   │   │   └── 📄 input.css         # Component classes and custom styles
│   │   ├── 📁 wwwroot/              # Static web assets
│   │   │   ├── 📁 css/              # Compiled Tailwind CSS
│   │   │   ├── 📁 js/               # Enhanced JavaScript (carousel, interactions)
│   │   │   └── 📁 images/           # Optimized images and media
│   │   ├── 📄 tailwind.config.js    # Tailwind configuration with custom colors
│   │   └── � package.json          # NPM dependencies for Tailwind
│   │   └── 📁 Styles/               # Tailwind CSS configuration
│   │
│   ├── 🚀 ShyvnTech.AppHost/        # .NET Aspire orchestration
│   │   ├── 📄 Program.cs            # Application host configuration
│   │   ├── 📄 azure.yaml            # Azure deployment configuration
│   │   └── 📄 next-steps.md         # Post-deployment guidance
│   │
│   ├── 📚 ShyvnTech.Magazine.Api/   # Magazine management API
│   │   ├── 📁 Controllers/          # API controllers
│   │   ├── 📁 wwwroot/pdfs/        # PDF storage (local files)
│   │   └── 📄 Program.cs            # API service configuration
│   │
│   ├── 📅 ShyvnTech.Events.Api/     # Events management API
│   ├── 🎓 ShyvnTech.Lms.Api/        # Learning management system API
│   ├── 📝 ShyvnTech.Content.Api/    # Content management API
│   ├── ⚙️ ShyvnTech.ApiService/     # General API services
│   │
│   └── 🔧 ShyvnTech.ServiceDefaults/ # Shared service configurations
│       └── 📄 Extensions.cs          # Common Aspire service defaults
│
├── 📄 Directory.Build.props          # MSBuild shared properties
├── 📄 Directory.Packages.props       # Centralized package management
├── 📄 ShyvnTech.sln                 # Visual Studio solution file
├── 📄 LICENSE                       # MIT license
└── 📄 README.md                     # This file
```

## 🛠️ Technology Stack

### **Frontend**

- **Blazor Server** - Interactive web UI framework
- **Tailwind CSS** - Utility-first CSS framework
- **Font Awesome** - Icon library
- **Custom CSS** - Enhanced styling and animations

### **Backend**

- **.NET 9.0** - Latest .NET framework
- **ASP.NET Core Web API** - RESTful API services
- **OpenAPI/Swagger** - API documentation
- **Scalar** - Modern API documentation UI

![Scalar API Documentation](docs/images/Scalar.PNG)
_Interactive API documentation powered by Scalar_

[OpenAPI Specification](https://localhost:7035/openapi/v1.json)

### **Architecture**

- **.NET Aspire** - Cloud-native application orchestration
- **Microservices** - Modular API architecture
- **Service Defaults** - Shared configurations and telemetry

### **Deployment**

- **Azure Container Apps** - Serverless container hosting
- **Azure Developer CLI (azd)** - Streamlined cloud deployment
- **GitHub Actions** - CI/CD pipeline support

## 🚀 Getting Started

### **Prerequisites**

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [Azure CLI](https://docs.microsoft.com/cli/azure/install-azure-cli) (for deployment)
- [Azure Developer CLI (azd)](https://learn.microsoft.com/azure/developer/azure-developer-cli/install-azd)

### **Local Development**

1. **Clone the repository**

   ```bash git clone https://github.com/your-org/shyvntech.git
   cd shyvntech
   ```

2. **Restore dependencies**

   ```bash
   dotnet restore
   ```

3. **Run the application**

   ```bash # Option 1: Run the entire solution with Aspire
   dotnet run --project src/ShyvnTech.AppHost/ShyvnTech.AppHost.csproj

   # Option 2: Run just the web application
   dotnet run --project src/ShyvnTech.Web/ShyvnTech.Web.csproj
   ```

4. **Access the application**
   - Web App: `https://localhost:7076`
   - Magazine API: `https://localhost:7208/swagger`
   - Events API: `https://localhost:7159/swagger`
   - Content API: `https://localhost:7134/swagger`
   - LMS API: `https://localhost:7298/swagger`

### **Using VS Code Tasks**

You can also use the predefined VS Code task:

```bash
# Press Ctrl+Shift+P and select "Tasks: Run Task"
# Then choose "Run ShyvnTech AppHost"
```

## ☁️ Deployment

### **Deploy to Azure**

1. **Authenticate with Azure**

   ```bash
   azd auth login --scope https://management.azure.com//.default
   ```

2. **Initialize the project**

   ```bash
   azd init
   ```

3. **Deploy to Azure**

   ```bash
   azd up
   ```

   This command will:

   - Provision Azure resources (Container Apps, Storage, etc.)
   - Build and containerize the applications
   - Deploy all services to Azure
   - Configure networking and dependencies

4. **Access your deployed application**
   ```text
   SUCCESS: Your app is ready for the cloud!
   Run azd up to provision and deploy your app to Azure.
   Run azd add to add new Azure components to your project.
   Run azd infra gen to generate IaC for your project to disk, allowing you to manually manage it.
   See ./next-steps.md for more information on configuring your app.
   ```

### **CI/CD Pipeline**

Configure automated deployment:

```bash
# GitHub Actions
azd pipeline config -e production

# Azure DevOps
azd pipeline config -e production --provider azdo
```

## 🤝 How to Contribute

We welcome contributions from developers, designers, and tech enthusiasts! Here's how you can get involved:

### **🎯 Ways to Contribute**

1. **🐛 Bug Reports** - Found an issue? Report it!
2. **✨ Feature Requests** - Have an idea? Share it!
3. **💻 Code Contributions** - Submit pull requests
4. **📚 Documentation** - Improve our docs
5. **🎨 Design** - Enhance UI/UX
6. **🧪 Testing** - Help with QA and testing

### **🚀 Getting Started with Contributions**

1. **Fork the repository**

   ```bash
   git fork https://github.com/your-org/shyvntech.git
   ```

2. **Create a feature branch**

   ```bash
   git checkout -b feature/amazing-feature
   ```

3. **Make your changes**

   - Follow our [development guide](docs/development-guide.md)
   - Write tests for new functionality
   - Update documentation as needed

4. **Test your changes**

   ```bash
   dotnet test
   dotnet run --project src/ShyvnTech.AppHost
   ```

5. **Submit a pull request**
   - Provide a clear description of changes
   - Reference any related issues
   - Ensure all checks pass

### **📋 Contribution Guidelines**

#### **Code Standards**

- Use **C# conventions** and **clean code principles**
- Follow **SOLID design patterns**
- Add **XML documentation** for public APIs
- Write **unit tests** for new features
- Use **semantic commit messages**

#### **Commit Message Format**

```
type(scope): description

Examples:
feat(api): add PDF download endpoint
fix(ui): resolve mobile navigation issue
docs(readme): update deployment instructions
style(css): improve hero section spacing
```

#### **Pull Request Process**

1. **Update documentation** for any public API changes
2. **Add tests** that prove your fix/feature works
3. **Ensure build passes** and all tests are green
4. **Update changelog** if applicable
5. **Get approval** from at least one maintainer

### **🎨 Design Contributions**

- **Figma designs** are welcome for new features
- **Accessibility** improvements are highly valued
- **Mobile-first** approach for responsive design
- **Brand consistency** with ShyvnTech identity

### **📚 Documentation Contributions**

- **Clear examples** and code samples
- **Step-by-step tutorials** for complex features
- **API documentation** improvements
- **Translation** to other languages

## 📞 Community & Support

### **🌐 Connect with Us**

- **Website**: [shyvntech.com](https://shyvntech.com)
- **GitHub Discussions**: [Join conversations](https://github.com/your-org/shyvntech/discussions)
- **LinkedIn**: [ShyvnTech Community](https://linkedin.com/company/shyvntech)
- **Twitter**: [@shyvntech](https://twitter.com/shyvntech)

### **💬 Getting Help**

- **📖 Documentation**: Check our [docs](docs/) folder
- **🐛 Issues**: [GitHub Issues](https://github.com/your-org/shyvntech/issues)
- **💭 Discussions**: [GitHub Discussions](https://github.com/your-org/shyvntech/discussions)
- **📧 Email**: support@shyvntech.com

### **📋 Development Resources**

- **[Development Guide](docs/development-guide.md)** - Setup and development workflow
- **[Architecture Guide](docs/architecture.md)** - System design and patterns
- **[API Documentation](docs/api-documentation.md)** - API reference and examples
- **[Deployment Guide](docs/deployment-guide.md)** - Production deployment instructions

## 📄 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- **Contributors** - Thank you to all our amazing contributors!
- **Open Source Community** - Built with ❤️ using open source technologies
- **Microsoft** - For .NET, Blazor, and Azure platform
- **Tailwind CSS** - For the excellent utility-first CSS framework

---

**Built with ❤️ by the ShyvnTech Tech Innovation Collective**

_Empowering the future of technology through innovation and continuous learning._
