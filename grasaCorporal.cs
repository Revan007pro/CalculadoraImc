using System;
using Gtk;

namespace grasaCorporal
{
    public class grasaCorporalApp
    {
        public VBox Content { get; private set; }

        public grasaCorporalApp()
        {
            Content = new VBox();

            // Grupo de género
            HBox generoBox = new HBox();
            RadioButton mujerButton = new RadioButton("Mujer");
            RadioButton hombreButton = new RadioButton(mujerButton, "Hombre");
            generoBox.PackStart(mujerButton, false, false, 5);
            generoBox.PackStart(hombreButton, false, false, 5);
            Content.PackStart(generoBox, false, false, 5);

            // Campos comunes
            Label label1 = new Label("Ingrese la circunferencia de su cintura (cm):");
            Content.PackStart(label1, false, false, 5);
            Entry cinturaEntry = new Entry();
            Content.PackStart(cinturaEntry, false, false, 5);

            Label label2 = new Label("Ingrese la circunferencia de su cuello (cm):");
            Content.PackStart(label2, false, false, 5);
            Entry cuelloEntry = new Entry();
            Content.PackStart(cuelloEntry, false, false, 5);

            Label label3 = new Label("Ingrese su altura (cm):");
            Content.PackStart(label3, false, false, 5);
            Entry alturaEntry = new Entry();
            Content.PackStart(alturaEntry, false, false, 5);

            
            Label label4 = new Label("Ingrese la circunferencia de sus caderas (cm):");
            Entry caderasEntry = new Entry();
            label4.Visible = false; 
            caderasEntry.Visible = false; 
            Content.PackStart(label4, false, false, 5);
            Content.PackStart(caderasEntry, false, false, 5);

            Button calcularButton = new Button("Calcular Grasa Corporal");
            Content.PackStart(calcularButton, false, false, 5);

            
            hombreButton.Toggled += (o, e) =>
            {
                if (hombreButton.Active)
                {
                    label4.Visible = false;
                    caderasEntry.Visible = false;
                }
            };

            mujerButton.Toggled += (o, e) =>
            {
                if (mujerButton.Active)
                {
                    label4.Visible = true;
                    caderasEntry.Visible = true;
                }
            };

            

            calcularButton.Clicked += (o, e) =>
            {
                if (float.TryParse(cinturaEntry.Text, out float cintura) &&
                    float.TryParse(cuelloEntry.Text, out float cuello) &&
                    float.TryParse(alturaEntry.Text, out float altura) &&
                    cintura > 0 && cuello > 0 && altura > 0)
                {
                    if (hombreButton.Active)
                    {
                        // Fórmula para hombres
                        float grasaCorporal = (cintura - cuello) / (altura / 100);
                        Label resultadoLabel = new Label($"Su Grasa Corporal es: {grasaCorporal:F2}%");
                        Content.PackStart(resultadoLabel, false, false, 5);
                        Content.ShowAll();
                    }
                    else if (mujerButton.Active)
                    {
                       
                        if (float.TryParse(caderasEntry.Text, out float caderas) && caderas > 0)
                        {
                            // Fórmula para mujeres
                            double grasaCorporal = 163.205 * Math.Log10(cintura + caderas - cuello) -
                                                   97.684 * Math.Log10(altura) - 78.387;
                            Label resultadoLabel = new Label($"Su Grasa Corporal es: {grasaCorporal:F2}%");
                            Content.PackStart(resultadoLabel, false, false, 5);
                            Content.ShowAll();
                        }
                        else
                        {
                            Label errorLabel = new Label("Por favor, ingrese un valor válido para las caderas.");
                            Content.PackStart(errorLabel, false, false, 5);
                            Content.ShowAll();
                        }
                    }
                }
                else
                {
                    Label errorLabel = new Label("Por favor, ingrese valores válidos.");
                    Content.PackStart(errorLabel, false, false, 5);
                    Content.ShowAll();
                }

                cinturaEntry.Text = "";
            cuelloEntry.Text = "";
            alturaEntry.Text = "";
            caderasEntry.Text = "";
            };
        }
    }
}
