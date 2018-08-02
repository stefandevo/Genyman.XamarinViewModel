# Stefandevo.Genyman.XamarinViewModel
Generating boilerplate code for Xamarin ViewModels
## Getting Started
Stefandevo.Genyman.XamarinViewModel is a **[genyman](http://genyman.net)** code generator. If you haven't installed **genyman** run following command:
```
dotnet tool install -g genyman
```
_Genyman is a .NET Core Global tool and thereby you need .NET Core version 2.1 installed._
## New Configuration file 
```
genyman new Stefandevo.Genyman.XamarinViewModel
```
## Sample Configuration file 
```
{
    "genyman": {
        "packageId": "Stefandevo.Genyman.XamarinViewModel",
        "info": "This is a Genyman configuration file - https://genyman.github.io/docs/"
    },
    "configuration": {
        "namespace": "Your.ClassNamespace",
        "name": "YourViewModel",
        "baseClass": "",
        "framework": "MugenMvvmToolkit",
        "properties": [
            {
                "name": "FirstName",
                "type": "string"
            },
            {
                "name": "LastName",
                "type": "string"
            }
        ],
        "commands": [
            {
                "name": "Save"
            },
            {
                "name": "Delete",
                "checkCanExecute": true
            }
        ]
    }
}
```
## Documentation 
### Class Configuration
| Name | Type | Req | Description |
| --- | --- | :---: | --- |
| Namespace | String | * | The namespace to use for the viewmodel |
| Name | String | * | The name of the viewmodel |
| BaseClass | String |  | The base class for the viewmodel |
| UseNPC | Boolean |  | Whether you want explicit INotifyPropertyChanged implemented |
| InjectedClasses | String[] |  | A list of classes that are injected in the constructor of the viewmodel |
| Framework | Framework (Enum) |  | For what mvvm framework this is used |
| Properties | Property[] |  | A list of properties |
| Commands | Command[] |  | A list of commands |
### Class Property
| Name | Type | Req | Description |
| --- | --- | :---: | --- |
| Name | String | * | The name of the property |
| Type | String | * | The type of the property |
| AlsoNotifyFor | String[] |  | A list of properties that also are notified when this property changes |
| DoNotNotify | Boolean |  | Do not generate a propertychanged when this property is changed |
| DoNotCheckEquality | Boolean |  | Do not check equality; always sends a propertychanged |
### Class Command
| Name | Type | Req | Description |
| --- | --- | :---: | --- |
| Name | String | * | Name of the command |
| Type | String |  | An optional type to associate to this command |
| CheckCanExecute | Boolean |  | Whether to generate a CanExecute clause for this command |
### Enum Framework
| Name | Description |
| --- | --- |
| XamarinForms | Xamarin Forms |
| MugenMvvmToolkit | Mugen MVVM Toolkit |
