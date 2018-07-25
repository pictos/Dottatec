# Teste para Dottatec

## Sobre o projeto

Para o desenvolvimento deste projeto foram utilizados:

- Xamarin.Forms, sem uso de frameworks adicionais;
- Design Pattern MVVM;
- Recursos para Coletar metricas do AppCenter;
- Mobile Apps do Azure, utilizando Easy Tables.

## Download do aplicativo

O aplicativo pode ser baixado em versões para Android(>= 4.4) e UWP. Como não possuo MAC a versão para iOS não estará disponível.

## Para fazer uso do código

Por motivos óbvios, eu retirei todas as chaves e links deste projeto, para utilizar basta fazer as devidas alterações.

Caso queria utilizar o AppCenter, basta adicionar o código abaixo na classe App.xaml.cs, se não quiser utilizar o AppCenter basta deletar os usings.

```csharp
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

 protected async override void OnStart()
 {
            // Handle when your app starts
    AppCenter.Start("android={Your Android App secret here}" +
               "uwp={Your UWP App secret here}" +
               "ios={Your iOS App secret here}", typeof(Analytics) typeof(Crashes));
    await BDAzure.Current.InitiAsync();
}
```

Para se conectar ao banco de dados deve inserir a Url do seu mobile app criado no azure. Isso dentro da classe Servicos.Const.

```csharp
class Const
{
    public const string Url = "https://seuServic.azurewebsites.net";
}
```
