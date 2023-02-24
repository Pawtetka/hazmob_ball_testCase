using System;
using System.Drawing;
using DefaultNamespace.Services;
using PlayfabServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ShopItemView : MonoBehaviour
    {
        [SerializeField] private Button buyBtn;
        [SerializeField] private Button equipBtn;
        [Header("ViewElements")]
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemPrice;
        [SerializeField] private TextMeshProUGUI equipText;

        public Button BuyButton => buyBtn;
        public Button EquipButton => equipBtn;

        private ShopItemState _currentState = ShopItemState.NotPurchased;
        public ShopItemState CurrentState => _currentState;

        public void Initialize(string name, int price, Currencies currency, int color, ShopItemState currentState)
        {
            itemName.text = name;
            itemPrice.text = price + " " + currency;
            itemImage.color = ColorsConverter.ToColor(color);
            _currentState = currentState;
            OnStateChanged();
        }

        public void ChangeState(ShopItemState newState)
        {
            _currentState = newState;
            OnStateChanged();
        }

        private void OnStateChanged()
        {
            switch (_currentState)
            {
                case ShopItemState.NotPurchased:
                    itemPrice.gameObject.SetActive(true);
                    equipBtn.gameObject.SetActive(false);
                    equipText.gameObject.SetActive(false);
                    break;
                case ShopItemState.Unequipped:
                    itemPrice.gameObject.SetActive(false);
                    equipText.text = "Equip";
                    equipBtn.gameObject.SetActive(true);
                    equipText.gameObject.SetActive(true);
                    break;
                case ShopItemState.Equipped:
                    itemPrice.gameObject.SetActive(false);
                    equipText.text = "Unequip";
                    equipBtn.gameObject.SetActive(true);
                    equipText.gameObject.SetActive(true);
                    break;
            }
        }
    }

    public enum ShopItemState
    {
        NotPurchased = 0,
        Unequipped = 1,
        Equipped = 2
    }
}