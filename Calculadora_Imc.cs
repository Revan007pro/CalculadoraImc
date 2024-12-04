using System;
using Gtk;
using Calorias;
using grasaCorporal;

namespace CalculadoraImc
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();

            Window window = new Window("Calculadora Salud");
            window.SetDefaultSize(400, 400);
            window.SetPosition(WindowPosition.Center);
            window.DeleteEvent += (o, e) => Application.Quit();

            VBox mainContainer = new VBox();
            window.Add(mainContainer);

            VBox contentArea = new VBox();
            mainContainer.PackStart(contentArea, true, true, 5);

            HBox menuBar = new HBox();
            mainContainer.PackStart(menuBar, false, false, 5);

            Button imcButton = new Button("IMC");
            menuBar.PackStart(imcButton, true, true, 5);

            Button caloriasButton = new Button("Calorías");
            menuBar.PackStart(caloriasButton, true, true, 5);

            Button grasaButton = new Button("Grasa Corporal");
            menuBar.PackStart(grasaButton, true, true, 5);

            VBox imcModule = CreateImcModule();
            CaloriasModule caloriasModule = new CaloriasModule();
            grasaCorporalApp grasaModule = new grasaCorporalApp(); // Asegúrate de que esto está instanciado correctamente

            contentArea.PackStart(imcModule, true, true, 5);

            
            imcButton.Clicked += (o, e) =>
            {
                contentArea.Remove(contentArea.Children[0]);
                contentArea.PackStart(imcModule, true, true, 5);
                contentArea.ShowAll();
            };

            caloriasButton.Clicked += (o, e) =>
            {
                contentArea.Remove(contentArea.Children[0]);
                contentArea.PackStart(caloriasModule.Content, true, true, 5);
                contentArea.ShowAll();
            };

            grasaButton.Clicked += (o, e) =>
            {
                contentArea.Remove(contentArea.Children[0]);
                contentArea.PackStart(grasaModule.Content, true, true, 5); // Asegúrate de que estás usando la propiedad `Content`
                contentArea.ShowAll();
            };

            window.ShowAll();
            Application.Run();
        }

        private static VBox CreateImcModule()
        {
            VBox vbox = new VBox();

            Label label1 = new Label("Ingrese su Peso (kg):");
            vbox.PackStart(label1, false, false, 5);

            Entry pesoEntry = new Entry();
            vbox.PackStart(pesoEntry, false, false, 5);

            Label label2 = new Label("Ingrese su Altura (m):");
            vbox.PackStart(label2, false, false, 5);

            Entry alturaEntry = new Entry();
            vbox.PackStart(alturaEntry, false, false, 5);

            Button calcularButton = new Button("Calcular IMC");
            vbox.PackStart(calcularButton, false, false, 5);

            Label resultadoLabel = new Label("Su IMC es:");
            vbox.PackStart(resultadoLabel, false, false, 5);

            Button limpiarButton = new Button("Nuevos Datos");
            vbox.PackStart(limpiarButton, false, false, 5);

            calcularButton.Clicked += (o, e) =>
            {
                if (float.TryParse(pesoEntry.Text, out float peso) &&
                    float.TryParse(alturaEntry.Text, out float altura) &&
                    altura > 0)
                {
                    float imc = peso / (altura * altura);

                    if (imc >= 25 || imc < 18.5)
                    {
                        resultadoLabel.Text = $"IMC fuera de rango (IMC: {imc:F2})";
                    }
                    else
                    {
                        resultadoLabel.Text = $"IMC normal (IMC: {imc:F2})";
                    }
                }
                else
                {
                    resultadoLabel.Text = "Datos inválidos. Verifique las entradas.";
                }
            };

            limpiarButton.Clicked += (o, e) =>
            {
                pesoEntry.Text = "";
                alturaEntry.Text = "";
                resultadoLabel.Text = "Su IMC es:";
                pesoEntry.GrabFocus();
            };

            return vbox;
        }
    }
}
