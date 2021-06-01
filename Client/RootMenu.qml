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
        property list<DonationData> donations;

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

                let temp = JSON.parse(JSON.stringify(parsed_transactions[i]))

                transaction.success = temp.success
                transaction.date_time = temp.dateTime
                transaction.from_card = temp.fromCard
                transaction.to_card = temp.toCard
                transaction.amount = temp.amount
                transaction.currency = temp.currency

                transactions.push(transaction);
            }
        }

        function update_donations(json_donations){
            let parsed_donations = JSON.parse(json_donations)
            donations = []

            for(var i = 0; i < parsed_donations.length; i++){
                var donation = Qt.createComponent("DonationData.qml");
                donation.title = parsed_donations[i].title
                donation.description = parsed_donations[i].description
                donation.donation_token = parsed_donations[i].donation_token
                donation.card_receiver = parsed_donations[i].card_receiver

                donations.push(donation);
                //console.log("from update: " + JSON.stringify(donation))
            }
        }

        function clear(){
            cards = []
            transactions = []
            donations = []
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
        interactive: false

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
