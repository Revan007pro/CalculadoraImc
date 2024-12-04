using System;
using Gtk;

namespace Calorias;
public class CaloriasModule
{
    public VBox Content { get; private set; }

    public CaloriasModule()
    {
        Content = new VBox();

        Label label1 = new Label("Ingrese su Peso (kg):");
        Content.PackStart(label1, false, false, 5);
        Entry pesoEntry = new Entry();
        Content.PackStart(pesoEntry, false, false, 5);

        Label label2 = new Label("Ingrese su Altura (cm):");
        Content.PackStart(label2, false, false, 5);
        Entry alturaEntry = new Entry();
        Content.PackStart(alturaEntry, false, false, 5);

        Label label3 = new Label("Ingrese su Edad:");
        Content.PackStart(label3, false, false, 5);
        Entry edadEntry = new Entry();
        Content.PackStart(edadEntry, false, false, 5);

        Label generoLabel = new Label("Seleccione su Género:");
        Content.PackStart(generoLabel, false, false, 5);

        HBox generoBox = new HBox();
        RadioButton hombreButton = new RadioButton("Hombre");
        RadioButton mujerButton = new RadioButton(hombreButton, "Mujer");
        generoBox.PackStart(hombreButton, false, false, 5);
        generoBox.PackStart(mujerButton, false, false, 5);
        Content.PackStart(generoBox, false, false, 5);

        

        Label label4 = new Label("Seleccione su Actividad Física:");
        Content.PackStart(label4, false, false, 5);

        HBox actividadBox = new HBox();
        RadioButton actividadPocoActivoButton = new RadioButton("Poco Activo");
        RadioButton actividadActivoButton = new RadioButton(actividadPocoActivoButton, "Activo");
        RadioButton actividadMuyActivoButton = new RadioButton(actividadPocoActivoButton, "Muy Activo");

        actividadBox.PackStart(actividadPocoActivoButton, false, false, 5);
        actividadBox.PackStart(actividadActivoButton, false, false, 5);
        actividadBox.PackStart(actividadMuyActivoButton, false, false, 5);
        Content.PackStart(actividadBox, false, false, 5);

        
        
        Button calcularButton = new Button("Calcular Calorías");
        Label resultadoLabel = new Label("Calorías estimadas:");
        Content.PackStart(resultadoLabel, false, false, 5);
        
        Content.PackStart(calcularButton, false, false, 5);

        calcularButton.Clicked += (o, e) =>
        {
            {
                
            }
            if (float.TryParse(pesoEntry.Text, out float peso) &&
                float.TryParse(alturaEntry.Text, out float altura) &&
                float.TryParse(edadEntry.Text, out float edad))
            {
                float calorias;
                float factorActividad = 1.0f;

                if (actividadPocoActivoButton.Active)
                {
                    factorActividad = 1.2f;
                }
               
                else if (actividadActivoButton.Active)
                {
                    factorActividad = 1.55f;
                }
                else if (actividadMuyActivoButton.Active)
                {
                    factorActividad = 1.9f;
                }
                else
                {
                    resultadoLabel.Text = "Seleccione un nivel de actividad física.";
                    return;
                }

                // Validar selección de género
                if (hombreButton.Active)
                {
                    calorias = ((10 * peso) + (6.25f * altura) - (5 * edad) + 5) * factorActividad;
                }
                else if (mujerButton.Active)
                {
                    calorias = ((10 * peso) + (6.25f * altura) - (5 * edad) - 161) * factorActividad;
                }
                else
                {
                    resultadoLabel.Text = "Seleccione un género.";
                     
                    return;
                }

                resultadoLabel.Text = $"Calorías diarias estimadas: {calorias:F2}";
               
            }
            else
            {
                resultadoLabel.Text = "Por favor, introduzca datos válidos.";
            }
        pesoEntry.Text = "";
        alturaEntry.Text = "";
        edadEntry.Text = "";
                    

                    
        };
    }
}
