using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;

namespace practice
{
    public partial class Form1 : Form
    {
        private readonly HttpClient client = new HttpClient();
        // Храним все курсы (полный список) для фильтрации
        private Dictionary<string, string> allRates = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();
            this.Text = "Курсы валют ЦБ РФ";
        }

        // === Обработчик для кнопки "Сегодня" ===
        private async void btnToday_Click(object sender, EventArgs e)
        {
            listBoxRates.Items.Clear();
            listBoxRates.Items.Add("Загрузка...");
            try
            {
                string url = "https://www.cbr-xml-daily.ru/daily_json.js";
                string json = await client.GetStringAsync(url);
                allRates = ParseRates(json);          // сохраняем все курсы
                DisplayRates(txtFilter.Text.Trim());  // отображаем с учётом фильтра
            }
            catch (Exception ex)
            {
                listBoxRates.Items.Clear();
                listBoxRates.Items.Add("Ошибка: " + ex.Message);
            }
        }

        // === Обработчик для кнопки "На дату" ===
        private async void btnDate_Click(object sender, EventArgs e)
        {
            listBoxRates.Items.Clear();
            listBoxRates.Items.Add("Загрузка...");
            try
            {
                string date = datePicker.Value.ToString("yyyy-MM-dd");
                string url = $"https://www.cbr-xml-daily.ru/daily_json.js?date={date}";

                // Для отладки – выведем URL в консоль 
                Console.WriteLine($"Запрос: {url}");

                string json = await client.GetStringAsync(url);
                allRates = ParseRates(json);
                DisplayRates(txtFilter.Text.Trim());
            }
            catch (Exception ex)
            {
                listBoxRates.Items.Clear();
                listBoxRates.Items.Add("Ошибка: " + ex.Message);
            }
        }

        // === Обработчик для кнопки "Фильтр" ===
        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Если есть загруженные курсы, применяем фильтр
            if (allRates.Count > 0)
                DisplayRates(txtFilter.Text.Trim());
            else
                MessageBox.Show("Сначала загрузите курсы (кнопка Сегодня или На дату).", "Информация");
        }

        // === Метод для отображения курсов с фильтром ===
        private void DisplayRates(string filter)
        {
            listBoxRates.Items.Clear();
            if (string.IsNullOrEmpty(filter))
            {
                foreach (var rate in allRates)
                    listBoxRates.Items.Add($"{rate.Key}: {rate.Value} руб.");  // добавляем здесь
            }
            else
            {
                bool found = false;
                foreach (var rate in allRates)
                {
                    if (rate.Key.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        rate.Value.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        listBoxRates.Items.Add($"{rate.Key}: {rate.Value} руб.");
                        found = true;
                    }
                }
                if (!found)
                    listBoxRates.Items.Add("Нет валют, соответствующих фильтру.");
            }
        }

        // === Парсинг JSON (возвращает словарь: название валюты -> строка курса) ===
        private Dictionary<string, string> ParseRates(string json)
        {
            var doc = JsonDocument.Parse(json);
            var valute = doc.RootElement.GetProperty("Valute");
            var result = new Dictionary<string, string>();
            foreach (var property in valute.EnumerateObject())
            {
                var value = property.Value;
                string name = value.GetProperty("Name").GetString() ?? "Неизвестно";
                string nominal = value.GetProperty("Nominal").GetInt32().ToString();
                string course = value.GetProperty("Value").GetDecimal().ToString("F4");
                result[name] = $"{nominal} {property.Name} = {course}"; 
            }
            return result;
        }
    }
}