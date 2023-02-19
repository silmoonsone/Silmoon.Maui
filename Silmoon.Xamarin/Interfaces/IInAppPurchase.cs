using System;
using System.Collections.Generic;
using System.Text;

namespace Silmoon.Xamarin.Interfaces
{
    public interface IInAppPurchase
    {
        bool LaunchPurchase(string ProductId, Action<string> LaunchedupPurchaseCallback, Action<InAppPurchaseResult> PurchaseCompletedCallback);
        bool LaunchPurchase(string ProductId, int Quantity, Action<string> LaunchedupPurchaseCallback, Action<InAppPurchaseResult> PurchaseCompletedCallback);
        bool RestorePurchase(Action<string> RestoreProductCallback, Action RestoreFinishedCallback);
    }
    public enum TransactionState : long
    {
        Purchasing = 0,
        Purchased = 1,
        Failed = 2,
        Restored = 3,
        Deferred = 4,
    }
    public class InAppPurchaseResult
    {
        public TransactionState State { get; set; }
        public string TransactionIdentifier { get; set; }
        public string TransactionReceipt { get; set; }
        public object Transaction { get; set; }
        public string ProductId { get; set; }
        public bool? Sandbox { get; set; }
    }
}
