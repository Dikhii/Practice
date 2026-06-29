using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;

namespace practice
{
    public partial class Form1 : Form
    {
        // Добавляем HttpClient как поле класса
        private readonly HttpClient client = new HttpClient();

        public Form1()
        {
            InitializeComponent();
        }

        // Обработчик для кнопки "Сегодня"
        private async void btnToday_Click(object sender, EventArgs e)
        {
            listBoxRates.Items.Clear();
            listBoxRates.Items.Add("Загрузка...");
            try
            {
                string url = "https://www.cbr-xml-daily.ru/daily_json.js";
                string json = await client.GetStringAsync(url);
                var rates = ParseRates(json);
                listBoxRates.Items.Clear();
                foreach (var rate in rates)
                    listBoxRates.Items.Add($"{rate.Key}: {rate.Value} руб.");
            }
            catch (Exception ex)
            {
                listBoxRates.Items.Clear();
                listBoxRates.Items.Add("Ошибка: " + ex.Message);
            }
        }

        // Обработчик для кнопки "На дату"
        private async void btnDate_Click(object sender, EventArgs e)
        {
            listBoxRates.Items.Clear();
            listBoxRates.Items.Add("Загрузка...");
            try
            {
                string date = datePicker.Value.ToString("dd/MM/yyyy");
                string url = $"https://www.cbr-xml-daily.ru/daily_json.js?date={date}";
                string json = await client.GetStringAsync(url);
                var rates = ParseRates(json);
                listBoxRates.Items.Clear();
                foreach (var rate in rates)
                    listBoxRates.Items.Add($"{rate.Key}: {rate.Value} руб.");
            }
            catch (Exception ex)
            {
                listBoxRates.Items.Clear();
                listBoxRates.Items.Add("Ошибка: " + ex.Message);
            }
        }

        // Метод парсинга JSON
        private Dictionary<string, string> ParseRates(string json)
        {
            var doc = JsonDocument.Parse(json);
            var valute = doc.RootElement.GetProperty("Valute");
            var result = new Dictionary<string, string>();
            foreach (var property in valute.EnumerateObject())
            {
                var value = property.Value;
                // Используем ?? чтобы избежать null-предупреждений
                string name = value.GetProperty("Name").GetString() ?? "Неизвестно";
                string nominal = value.GetProperty("Nominal").GetInt32().ToString();
                string course = value.GetProperty("Value").GetDecimal().ToString("F4");
                result[name] = $"{nominal} {property.Name} = {course} руб.";
            }
            return result;
        }
    }
}