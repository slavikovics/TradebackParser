using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TradebackParser.ViewModels;

public partial class MultiplierViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isFirstCountRuleActive = true;
    
    [ObservableProperty]
    private bool _isSecondCountRuleActive = true;
    
    [ObservableProperty]
    private bool _isPhraseFilterActive = true;

    [ObservableProperty]
    private double _firstCountValue;
    
    [ObservableProperty]
    private double _secondCountValue;
    
    [ObservableProperty] 
    private string _phrasesFilter = string.Empty;

    [ObservableProperty]
    private int _selectedIndex = 0;
    
    [ObservableProperty]
    private double _multiplierValue = 0.55;
    
    public ObservableCollection<string> Phrases { get; set; }
    
    public List<ItemModel> Items { get; set; }

    public event EventHandler SwitchToDownloadsEvent;

    public MultiplierViewModel(List<ItemModel> items)
    {
        Items = items;
        Phrases = new ObservableCollection<string>();
    }

    partial void OnPhrasesFilterChanged(string phrase)
    {
        Phrases.Clear();
        FindPhrases().ForEach(x => Phrases.Add(x));
    }

    private void RemoveFirstCount()
    {
        if (!IsFirstCountRuleActive) return;
        List<ItemModel> itemsToRemove = new List<ItemModel>();
        
        foreach (var item in Items)
        {
            if (item.Count1 < FirstCountValue)
            {
                itemsToRemove.Add(item);
            }
        }
        
        itemsToRemove.ForEach(item => Items.Remove(item));
    }

    private void RemoveSecondCount()
    {
        if (!IsSecondCountRuleActive) return;
        List<ItemModel> itemsToRemove = new List<ItemModel>();
        
        foreach (var item in Items)
        {
            if (item.Count2 < SecondCountValue)
            {
                itemsToRemove.Add(item);
            }
        }
        
        itemsToRemove.ForEach(item => Items.Remove(item));
    }

    private List<string> FindPhrases()
    {
        List<string> phrases = QuotedStringParser.ParseQuotedTokens(PhrasesFilter);

        for (int i = 0; i < phrases.Count; i++)
        {
            phrases[i] = phrases[i].Trim(' ');
            phrases[i] = phrases[i].Replace("\"", "");
        }

        return phrases;
    }

    private void RemovePhraseFilter()
    {
        var phrases = FindPhrases();
        List<ItemModel> itemsToRemove = new List<ItemModel>();
        
        foreach (var item in Items)
        {
            foreach (var phrase in phrases)
            {
                if (item.Name.Contains(phrase))
                {
                    itemsToRemove.Add(item);
                }
            }
        }
        
        itemsToRemove.ForEach(item => Items.Remove(item));
    }

    private double CalculatePrice(ItemModel item)
    {
        switch (SelectedIndex)
        {
            case 0: return (double) item.Price1;
            case 1: return (double) item.Price2;
            case 2: return ((double) item.Price1 + (double) item.Price2) / 2.0;
        }
        return (double) item.Price1;
    }

    private void CountProposedPrices()
    {
        foreach (ItemModel item in Items)
        {
            var price = CalculatePrice(item) * MultiplierValue;
            item.ProposedPrice = Convert.ToDecimal(Math.Round(price * 100));
        }
    }

    [RelayCommand]
    private void SwitchToDownloads()
    {
        RemoveFirstCount();
        RemoveSecondCount();
        RemovePhraseFilter();
        CountProposedPrices();
        
        SwitchToDownloadsEvent?.Invoke(this, new CustomEventArgs(Items.ToList()));
    }
}