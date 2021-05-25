import QtQuick 2.0
import QtQuick.Controls 2.12

Page {
    id: transactionsRoot

    ListView{
        id: transactionsView
        model: clientUserData.transactions

        anchors{
            fill: parent
        }

        spacing: 2

        delegate: Rectangle{
            height: 80
            width: parent.width*0.95
            radius: 10

            anchors{
                horizontalCenter: parent.horizontalCenter
            }

            color: "gray"

            Text{
                id: fromCardLable
                text: "from: " + from_card
                color: "white"
                font.pointSize: 10

                anchors{
                    top: parent.top
                    topMargin: 10
                    left: parent.left
                    leftMargin: 10
                }
            }

            Text{
                id: toCardLable
                text: "to: " + to_card
                color: "white"
                font.pointSize: 10

                anchors{
                    bottom: parent.bottom
                    bottomMargin: 10
                    left: parent.left
                    leftMargin: 10
                }
            }

            Text{
                id: amountLabel
                text: amount + " " + currency
                color: "white"
                font.pointSize: 10

                anchors{
                    verticalCenter: parent.verticalCenter
                    right: parent.right
                    rightMargin: 10
                }
            }

            Text{
                id: dateTimeLabel
                text: date_time
                color: "white"
                font.pointSize: 7

                anchors{
                    horizontalCenter: parent.horizontalCenter
                    bottom: parent.bottom
                    bottomMargin: 10
                }
            }

        }

        // @disable-check M16
        Component.onCompleted: {
            clientUserData.update_transactions(api.transactions(clientUserData.token))
            transactionsView.model = clientUserData.transactions
            transactionsView.positionViewAtEnd()
        }
    }
}

/*##^##
Designer {
    D{i:0;autoSize:true;formeditorZoom:0.75;height:480;width:640}
}
##^##*/
