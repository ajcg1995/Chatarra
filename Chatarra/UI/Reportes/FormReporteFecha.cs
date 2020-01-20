﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Reportes
{
    public partial class FormReporteFecha : Form
    {
        public Control control { get; set; }

        public FormReporteFecha(Control control)
        {
            InitializeComponent();
            this.control = control;
        }

        private void FormReporteFecha_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'chatarraDataSet.SpReportePorFecha' Puede moverla o quitarla según sea necesario.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'chatarraDataSet.SpReportePorFecha' Puede moverla o quitarla según sea necesario.
            this.spReportePorFechaTableAdapter.Fill(this.chatarraDataSet.SpReportePorFecha,dtDesde.Value,dtHasta.Value);

            this.reportViewer1.RefreshReport();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            control.AbrirFormPrincipal();

            this.Visible = false;
        }
    }
}
