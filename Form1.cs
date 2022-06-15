namespace FormPokedex
{
    public partial class Form1 : Form
    {
        int idPokemon = 1;
        public Form1()
        {
            InitializeComponent();
            CargaInicial();
        }

        private async void CargaInicial()
        {
            textID.BackColor = ColorTranslator.FromHtml("#55ab62");
            CargaInicialLabels(Color.Red);
            await Leer(idPokemon);
            textID.Text = idPokemon.ToString();
        }

        private void CargaInicialLabels(Color _color)
        {
            label1.ForeColor = _color;
            label2.ForeColor = _color;
            label4.ForeColor = _color;
            label6.ForeColor = _color;
            label8.ForeColor = _color;
            label10.ForeColor = _color;
            label12.ForeColor = _color;
            labelAltura.ForeColor = _color;
            labelAtaque.ForeColor = _color;
            labelDefensa.ForeColor = _color;
            labelVelocidad.ForeColor = _color;
            labelVida.ForeColor = _color;
            labelMov1.ForeColor = _color;
            labelMov2.ForeColor = _color;
            labelMov3.ForeColor = _color;
            labelMov4.ForeColor = _color;
            labelPeso.ForeColor = _color;
            labelAltura.ForeColor = _color;

            labelNombre.Text = "";
            labelNumero.Text = "";
            labelTipo1.Text = "";
            labelTipo2.Text = "";
            labelAltura.Text = "";
            labelAtaque.Text = "";
            labelDefensa.Text = "";
            labelVelocidad.Text = "";
            labelVida.Text = "";
            labelMov1.Text = "";
            labelMov2.Text = "";
            labelMov3.Text = "";
            labelMov4.Text = "";
            labelPeso.Text = "";
            labelAltura.Text = "";

            VisibilidadLabels(false);
        }

        private void VisibilidadLabels(bool _visible)
        {
            label1.Visible = _visible;
            label2.Visible = _visible;
            label4.Visible = _visible;
            label6.Visible = _visible;
            label8.Visible = _visible;
            label10.Visible = _visible;
            label12.Visible = _visible;
        }

        private async void btnBusqueda_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textID.Text))
            {
                idPokemon = int.Parse(textID.Text);
                await Leer(idPokemon);
            }            
        }

        private async Task Leer(int _valor)
        {
            try
            {
                var client = new HttpClient();
                var result = await client.GetStringAsync("https://pokeapi.co/api/v2/pokemon/" + _valor.ToString());
                var pokemoncito = Pokedex.FromJson(result);
                CargarTodo(pokemoncito);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pokemon no encontrado");
            }            
        }

        private void CargarTodo(Pokedex? pokemon)
        {
            CargarImagen(pokemon);
            CargarNombre(pokemon.Name.ToUpper());
            CargarNumero(pokemon.Id.ToString());
            CargarStats(pokemon);
            CargarMovimientos(pokemon);
            CargarTipos(pokemon);
            VisibilidadLabels(true);
        }

        private void CargarMovimientos(Pokedex? pokemon)
        {
            labelMov1.Text = "- " + pokemon.Moves[0].MoveMove.Name.ToUpper();
            labelMov2.Text = "- " + pokemon.Moves[1].MoveMove.Name.ToUpper();
            labelMov3.Text = "- " + pokemon.Moves[2].MoveMove.Name.ToUpper();
            labelMov4.Text = "- " + pokemon.Moves[3].MoveMove.Name.ToUpper();
        }

        private void CargarImagen(Pokedex? pokemon)
        {
            pictureFront.Load(pokemon.Sprites.FrontDefault.ToString());
            pictureBack.Load(pokemon.Sprites.BackDefault.ToString());
            pictureIlustrado.Load(pokemon.Sprites.Other.OfficialArtwork.FrontDefault.ToString());
            pictureFront.Update();
            pictureIlustrado.Update();
        }

        private void CargarNombre(string _nombre)
        {
            labelNombre.Text = _nombre;
            labelNombre.Left = (panelNombre.Width / 2) - (labelNombre.Width / 2);           
            labelNombre.Update();
        }

        private void CargarNumero(string _numero)
        {
            labelNumero.Text = _numero;
            labelNumero.Left = (panelNombre.Width / 2) - (labelNumero.Width / 2);
            labelNumero.Update();
        }

        private void CargarStats(Pokedex? pokemon)
        {
            labelPeso.Text = (pokemon.Weight / 10).ToString() + " kg";
            labelAltura.Text = (pokemon.Height * 10).ToString() + " cm";
            labelAtaque.Text = pokemon.Stats[1].BaseStat.ToString();
            labelVida.Text = pokemon.Stats[0].BaseStat.ToString();
            labelDefensa.Text = pokemon.Stats[2].BaseStat.ToString();
            labelVelocidad.Text = pokemon.Stats[5].BaseStat.ToString();
        }

        private void CargarTipos(Pokedex? pokemon)
        {
            //pokemoncito.Types[0].Type.Name
            labelTipo1.Text = pokemon.Types[0].Type.Name.ToUpper();
            if(pokemon.Types.Length > 1)
            {
                labelTipo2.Text = pokemon.Types[1].Type.Name.ToUpper();
            }
            else
            {
                labelTipo2.Text = "";
            }

            AjustarPosicionTipos();
            AsignarColorTipos();
        } 
        
        private void AjustarPosicionTipos()
        {
            labelTipo1.Left = (panelTipos.Width / 4) - (labelTipo1.Width / 2);
            labelTipo1.Update();
            labelTipo2.Left = ((panelTipos.Width / 2) + (panelTipos.Width / 4)) - (labelTipo2.Width / 2);
            labelTipo2.Update();
        }

        private void AsignarColorTipos()
        {
            labelTipo1.BackColor = AjustarColorTipos(labelTipo1.Text)[0];
            labelTipo1.ForeColor = AjustarColorTipos(labelTipo1.Text)[1];
            labelTipo2.BackColor = AjustarColorTipos(labelTipo2.Text)[0];
            labelTipo2.ForeColor = AjustarColorTipos(labelTipo2.Text)[1];
        }

        private Color[] AjustarColorTipos(string _tipo)
        {
            Color[] colores = new Color[2];            
            colores[0] = Color.Transparent;
            colores[1] = Color.Gold;

            string tipo = _tipo;

            switch (tipo)
            {
                case "ELECTRIC":
                    colores[0] = ColorTranslator.FromHtml("#fafa71");
                    colores[1] = Color.White;
                    break;
                case "POISON":
                    colores[0] = ColorTranslator.FromHtml("#9a69d8");
                    colores[1] = Color.White;
                    break;
                case "GRASS":
                    colores[0] = ColorTranslator.FromHtml("#26c94f");
                    colores[1] = Color.White;
                    break;
                case "FIRE":
                    colores[0] = ColorTranslator.FromHtml("#fb4b5a");
                    colores[1] = Color.White;
                    break;
                case "FLYING":
                    colores[0] = ColorTranslator.FromHtml("#92b1c6");
                    colores[1] = Color.White;
                    break;
                case "WATER":
                    colores[0] = ColorTranslator.FromHtml("#85a7fb");
                    colores[1] = Color.White;
                    break;
                case "BUG":
                    colores[0] = ColorTranslator.FromHtml("#3b984f");
                    colores[1] = Color.White;
                    break;
                case "DARK":
                    colores[0] = ColorTranslator.FromHtml("#595979");
                    colores[1] = Color.White;
                    break;
                case "DRAGON":
                    colores[0] = ColorTranslator.FromHtml("#60c9d8");
                    colores[1] = Color.White;
                    break;
                case "FAIRY":
                    colores[0] = ColorTranslator.FromHtml("#ea1268");
                    colores[1] = Color.White;
                    break;
                case "FIGHTING":
                    colores[0] = ColorTranslator.FromHtml("#ee6137");
                    colores[1] = Color.White;
                    break;
                case "ICE":
                    colores[0] = ColorTranslator.FromHtml("#86d2f5");
                    colores[1] = Color.White;
                    break;
                case "GROUND":
                    colores[0] = ColorTranslator.FromHtml("#6d481e");
                    colores[1] = Color.White;
                    break;
                case "STEEL":
                    colores[0] = ColorTranslator.FromHtml("#40bd93");
                    colores[1] = Color.White;
                    break;
                case "ROCK":
                    colores[0] = ColorTranslator.FromHtml("#8a3d21");
                    colores[1] = Color.White;
                    break;
                case "GHOST":
                    colores[0] = ColorTranslator.FromHtml("#33336b");
                    colores[1] = Color.White;
                    break;
                case "PSYCHIC":
                    colores[0] = ColorTranslator.FromHtml("#f71b90");
                    colores[1] = Color.White;
                    break;
                case "NORMAL":
                    colores[0] = ColorTranslator.FromHtml("#74505a");
                    colores[1] = Color.White;
                    break;
            }

            return colores;
        }

        private async void labelSiguiente_Click(object sender, EventArgs e)
        {
            if(idPokemon + 1 >= 899)
            {
                idPokemon = 1;
            }
            else
            {
                idPokemon++;
            }
            await Leer(idPokemon);
            textID.Text = idPokemon.ToString();
        }

        private async void labelAnterior_Click(object sender, EventArgs e)
        {
            if(idPokemon - 1 <= 0)
            {
                idPokemon = 898;
            }
            else
            {
                idPokemon--;
            }
            await Leer(idPokemon);
            textID.Text = idPokemon.ToString();
        }
    }
}