using System.Net;
using System.Text.Json;

namespace _4M09mauiKursBitcoina
{

    public class BitcoinRate
    {
        public string? code { get; set; }
        public string? description { get; set; }
        public double? rate_float { get; set; }
    }
    public class BitcoinRate2
    {
        public BitcoinRate? USD { get; set; }
        public BitcoinRate? GBP { get; set; }
        public BitcoinRate? EUR { get; set; }
    }
    public class Bitcoin
    {
        public string? chartName { get; set; }
        public BitcoinRate2 bpi { get; set; }
    }
    public class USD
    {
        public string? code { get; set; }
        public IList<Rate> rates { get; set; }
    }
    public class Rate
    {
        public double? ask { get; set; }
        public double? bid { get; set; }
    }
    public partial class MainPage : ContentPage
    {
       

        public MainPage()
        {
            double usd = 0;
            InitializeComponent();
            string dzis = DateTime.Now.ToString("yyyy-MM-dd");
            string urlUSD = "https://api.nbp.pl/api/exchangerates/rates/c/usd/"+dzis+"/?format=json";
            string json;
            using(var webClient = new WebClient())
            {
                json = webClient.DownloadString(urlUSD);
            }
            USD dolar = JsonSerializer.Deserialize<USD>(json);
            usd = (double)dolar.rates[0].ask;
            //lblUSD.Text = usd.ToString();

            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";
            using(var webClient = new WebClient())
            {
                json = webClient.DownloadString(url);
            }
            Bitcoin bitcoin = JsonSerializer.Deserialize<Bitcoin>(json);

            string s = (string)bitcoin.bpi.USD.code + ": ";
            s += ((double)bitcoin.bpi.USD.rate_float).ToString("# ###.####");
            lblUSD.Text = s;
            s = (string)bitcoin.bpi.GBP.code + ": ";
            s += ((double)bitcoin.bpi.GBP.rate_float).ToString("# ###.####");
            lblGBP.Text = s;
            s = (string)bitcoin.bpi.EUR.code + ": ";
            s += ((double)bitcoin.bpi.EUR.rate_float).ToString("# ###.####");
            lblEUR.Text = s;
            s = "PLN: ";
            s += (usd * (double)bitcoin.bpi.USD.rate_float).ToString("# ###.####");
            lblPLN.Text = s;
        }

        

    }

}
