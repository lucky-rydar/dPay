import QtQuick 2.12
import QtQuick.Window 2.12
import QtQuick.Controls.Styles 1.4
import QtQuick.Controls.Universal 2.12
import QtQuick.Controls 2.12
import api.wrapper 2.0
import cpp.modules 2.0

Item {
    id: rootMenu

    ClientUserData{
        id: clientUserData

        property list<CardData> cards;
        property list<TransactionData> transactions;

        function update_cards(json_cards) {
            let parsed_cards = JSON.parse(json_cards)
            cards = []
            for(var i = 0; i < parsed_cards.length; i++){
                var card = Qt.createComponent("CardData.qml");
                card.id = parsed_cards[i].id
                card.name = parsed_cards[i].name
                card.card_token = parsed_cards[i].card_token
                card.balance = parsed_cards[i].balance
                card.currency = parsed_cards[i].currency
                card.is_default = parsed_cards[i].is_default

                cards.push(card);
                console.log(JSON.stringify(parsed_cards[i]))
            }

            console.log(JSON.stringify(cards[0]))
        }

        function update_transactions(json_transactions){
            let parsed_transactions = JSON.parse(json_transactions)
            transactions = []

            for(var i = 0; i < parsed_transactions.length; i++){
                var transaction = Qt.createComponent("TransactionData.qml");
                transaction.success = parsed_transactions[i].success
                transaction.date_time = parsed_transactions[i].date_time
                transaction.from_card = parsed_transactions[i].from_card
                transaction.to_card = parsed_transactions[i].to_card
                transaction.amount = parsed_transactions[i].amount
                transaction.currency = parsed_transactions[i].currency

                transactions.push(transaction);
                console.log("from update: " + JSON.stringify(transaction))
            }
        }

        function clear(){
            cards = []
            token = ""
            username = ""
            phone = ""
            email = ""
        }
    }

    ApiWrapper{
        id: api
    }

    SwipeView{
        id: rootSwipeView
        anchors.fill: parent
        currentIndex: 1
        //interactive: false //TODO: uncomment on release

        Register{
            id: regPage
        }

        Login{
            id: logPage
        }

        MainMenu{
            id: mainPage
        }
    }
}
/*##^##
Designer {
    D{i:0;autoSize:true;formeditorZoom:0.6600000262260437;height:480;width:640}
}
##^##*/
