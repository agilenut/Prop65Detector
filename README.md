<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/agilenut/prop65detector">
    <img src="docs/images/hat.png" alt="Logo">
  </a>

  <h3 align="center">Prop 65 Detector</h3>

  <p align="center">
    A small Blazor Web Assembly and API sample
    <br />
    <br />
  </p>
</p>



<!-- TABLE OF CONTENTS -->
## Table of Contents

* [About the Project](#about-the-project)
  * [Built With](#built-with)
* [Getting Started](#getting-started)
  * [Prerequisites](#prerequisites)
  * [Installation](#installation)
* [Usage](#usage)
* [Contributing](#contributing)
* [License](#license)
* [Contact](#contact)
* [Acknowledgements](#acknowledgements)



<!-- ABOUT THE PROJECT -->
## About The Project

This to upload Safety Data Sheets in PDF format, parse for CAS numbers and test if they are on the California Prop 65 list.


### Built With

* [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)



<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these steps.

### Prerequisites

1. Requires Visual Studio 2019

### Installation
 
1. Clone the prop65detector repository
```sh
git clone https://github.com/agilenut/prop65detector.git
```
2. Run in Visiual Studio 2019


<!-- USAGE -->
## Usage

Drag and drop files to the drag area or click to browse for PDFs. Uploaded PDFs will be parsed for section 3 of the 
material Safety Data Sheet. If found, CAS numbers will be matched with ingredient CAS numbers in the Prop 65 list.

<!-- ROADMAP -->
## Roadmap

See the [open issues](https://github.com/agilenut/prop65detector/issues) for a list of proposed features (and known issues).



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.


<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements

* [BlazorInputFile](https://github.com/SteveSandersonMS/BlazorInputFile)
* [iText7-dotnet](https://github.com/itext/itext7-dotnet)





<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[product-screenshot]: docs/images/screenshot.png