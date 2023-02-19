using Foundation;
using Silmoon.Xamarin.Interfaces;
using Silmoon.Xamarin.iOS;
using StoreKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(InAppPurchase))]
namespace Silmoon.Xamarin.iOS
{
    public class InAppPurchase : SKProductsRequestDelegate, IInAppPurchase
    {
        Action<string> OnLaunchedupPurchaseEvent;
        Action<InAppPurchaseResult> OnPurchaseCompleted;

        Action<string> OnRestoreEvent;
        Action OnRestoreFinishedEvent;

        InAppPurchasePaymentObserver inAppPurchasePaymentObserver;
        public bool InPurchasing { get => OnPurchaseCompleted != null; }
        public InAppPurchase()
        {
            inAppPurchasePaymentObserver = new InAppPurchasePaymentObserver(this);
            SKPaymentQueue.DefaultQueue.AddTransactionObserver(inAppPurchasePaymentObserver);
        }
        public bool LaunchPurchase(string ProductId, Action<string> LaunchedupPurchaseCallback, Action<InAppPurchaseResult> PurchaseCompletedCallback)
        {
            if (InPurchasing) return false;
            SKPayment payment = SKPayment.CreateFrom(ProductId);
            OnLaunchedupPurchaseEvent = LaunchedupPurchaseCallback;
            OnPurchaseCompleted = PurchaseCompletedCallback;
            SKPaymentQueue.DefaultQueue.AddPayment(payment);
            return true;
        }
        public bool LaunchPurchase(string ProductId, int Quantity, Action<string> LaunchedupPurchaseCallback, Action<InAppPurchaseResult> PurchaseCompletedCallback)
        {
            if (Quantity == 1) return LaunchPurchase(ProductId, LaunchedupPurchaseCallback, PurchaseCompletedCallback);
            else
            {
                if (InPurchasing) return false;
                SKMutablePayment payment = SKMutablePayment.PaymentWithProduct(ProductId);
                payment.Quantity = Quantity;
                OnLaunchedupPurchaseEvent = LaunchedupPurchaseCallback;
                OnPurchaseCompleted = PurchaseCompletedCallback;
                SKPaymentQueue.DefaultQueue.AddPayment(payment);
                return true;
            }
        }
        public bool RestorePurchase(Action<string> RestoreProductCallback, Action RestoreFinishedCallback)
        {
            if (InPurchasing) return false;
            OnRestoreEvent = RestoreProductCallback;
            OnRestoreFinishedEvent = RestoreFinishedCallback;

            SKPaymentQueue.DefaultQueue.RestoreCompletedTransactions();
            return true;
        }

        public override void ReceivedResponse(SKProductsRequest request, SKProductsResponse response)
        {

        }


        internal void onInAppPurchaseEvent(SKPaymentTransaction transaction, TransactionState State, string TransactionReceipt, bool? SandBox)
        {
            if (State == TransactionState.Purchasing)
            {
                OnLaunchedupPurchaseEvent?.Invoke(transaction.Payment.ProductIdentifier);
                return;
            }

            var e = new InAppPurchaseResult()
            {
                State = State,
                Transaction = transaction,
                TransactionIdentifier = transaction.TransactionIdentifier,
                TransactionReceipt = TransactionReceipt,
                ProductId = transaction?.Payment?.ProductIdentifier,
                Sandbox = SandBox,
            };


            if (State == TransactionState.Purchased || State == TransactionState.Failed || State == TransactionState.Deferred)
            {
                OnPurchaseCompleted?.Invoke(e);
                OnLaunchedupPurchaseEvent = null;
                OnPurchaseCompleted = null;
                SKPaymentQueue.DefaultQueue.FinishTransaction(transaction);
            }
            else if (State == TransactionState.Restored)
            {
                onRestoreEvent(transaction);
                SKPaymentQueue.DefaultQueue.FinishTransaction(transaction);
            }
        }
        internal void onRestoreEvent(SKPaymentTransaction transaction)
        {
            OnRestoreEvent?.Invoke(transaction.OriginalTransaction.Payment.ProductIdentifier);
        }
        internal void onRestoreFinishedEvent(SKPaymentQueue queue, NSError error)
        {
            OnRestoreFinishedEvent?.Invoke();
            OnRestoreEvent = null;
            OnRestoreFinishedEvent = null;
        }
    }

    public class InAppPurchasePaymentObserver : SKPaymentTransactionObserver
    {
        InAppPurchase inAppPurchase;
        public InAppPurchasePaymentObserver(InAppPurchase inAppPurchase) => this.inAppPurchase = inAppPurchase;
        public override void UpdatedTransactions(SKPaymentQueue queue, SKPaymentTransaction[] transactions)
        {
            foreach (SKPaymentTransaction transaction in transactions)
            {
                var receipt = transaction?.TransactionReceipt;
                bool? sandbox = null;
                if (receipt != null) sandbox = receipt.ToString().ToLower().Contains("sandbox");

                inAppPurchase.onInAppPurchaseEvent(transaction, (TransactionState)transaction.TransactionState, receipt?.GetBase64EncodedString(NSDataBase64EncodingOptions.None), sandbox);
            }
        }
        public override void RestoreCompletedTransactionsFinished(SKPaymentQueue queue)
        {
            inAppPurchase.onRestoreFinishedEvent(queue, null);
        }

        public override void RestoreCompletedTransactionsFailedWithError(SKPaymentQueue queue, NSError error)
        {
            inAppPurchase.onRestoreFinishedEvent(queue, error);
        }
    }
}