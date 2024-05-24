using BASE.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BASE
{
    public partial class Form1 : Form
    {
        private string[] razasDragonBall = {
            "Android",
            "Bio-Android",
            "Humana",
            "Humano",
            "Majin",
            "Namekuseijin",
            "Saiyajin",
            "Saiyajin/Humano",
            "Saiyajin/Saiyajin"
    };
        // DEFINIMOS UNA VARIABLE DE INSTANCIA
        private PersonajeDB personaje;
        public Form1()
        {
            InitializeComponent();
            personaje = new PersonajeDB();
        }

        private void prueba_Click(object sender, EventArgs e)
        {
            if (personaje.ProbarConexion())
            {
                MessageBox.Show("CONEXION EXITOSA");
            }
            else
            {
                MessageBox.Show("CONEXION FALLIDA");
            }
        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
            DataTable dt = personaje.LeerPersonajes();
            dataGridView1.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxRaza.Items.AddRange(razasDragonBall);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarPorID();
        }

        private void txtboxID_Leave(object sender, EventArgs e)
        {
            BuscarPorID();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            string nombre = txtboxNombre.Text;
            string raza = comboBoxRaza.Text;
            int nivelPoder = (int)NivelPoder.Value;
            DateTime fecha_creacion = DateTime.Now; // MODIFICADO
            string historia = txtboxHistoria.Text;


            int respuesta = personaje.CrearPersonaje(nombre, raza, nivelPoder, fecha_creacion, historia);
            if (respuesta > 0) {
                MessageBox.Show("CREADO CON EXITO");
                dataGridView1.DataSource = personaje.LeerPersonajes();
            }
            else
            {
                MessageBox.Show("HUBO UN ERROR");
            }
        }
        private void BuscarPorID()
        {
            int idPersonajeBuscar = int.Parse(txtboxID.Text);
            DataTable personajeEncontado = personaje.BuscarPersonajePorId(idPersonajeBuscar);
            if (personajeEncontado.Rows.Count > 0)
            {
                string nombre = personajeEncontado.Rows[0]["nombre"].ToString();
                string raza = personajeEncontado.Rows[0]["raza"].ToString();
                int nivelPoder = int.Parse(personajeEncontado.Rows[0]["nivel_poder"].ToString());
                DateTime fecha_creacion = (DateTime)personajeEncontado.Rows[0]["fecha_creacion"]; // MODIFICADO
                string historia = personajeEncontado.Rows[0]["historia"].ToString();

                txtboxNombre.Text = nombre;
                comboBoxRaza.Text = raza;
                NivelPoder.Value = nivelPoder;
                timerFecha.Value = fecha_creacion; // MODIFICADO
                txtboxHistoria.Text = historia;
            }
            else
            {
                MessageBox.Show("ID NO EXISTENTE");
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e) // OPCION CREADA
        {
            int id = Convert.ToInt32(txtboxID.Text);
            string nombre = txtboxNombre.Text;
            string raza = comboBoxRaza.Text;
            int nivelPoder = (int)NivelPoder.Value;
            string historia = txtboxHistoria.Text;
            int respuesta = personaje.ActualizarPersonaje(id, nombre, raza, nivelPoder, historia);
            if (respuesta > 0)
            {
                MessageBox.Show("ACTUALIADO CON EXITO");
                dataGridView1.DataSource = personaje.LeerPersonajes();
            }
            else
            {
                MessageBox.Show("HUBO UN ERROR");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e) // OPCION CREADA
        {
            int id = Convert.ToInt32(txtboxID.Text);
            int respuesta = personaje.BorrarPersonaje(id);
            if (respuesta > 0)
            {
                MessageBox.Show("ELIMINADO CON EXITO");
                txtboxID.Clear();
                txtboxNombre.Clear();
                comboBoxRaza.SelectedIndex = -1;
                NivelPoder.Value = 0;
                txtboxHistoria.Clear();
                dataGridView1.DataSource = personaje.LeerPersonajes();
            }
            else
            {
                MessageBox.Show("HUBO UN ERROR");
            }
        }
    }
}
