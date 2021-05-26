import QtQuick 2.0
import QtQuick.Controls 2.0
import QtQuick.Dialogs 1.2

Page {
    ListView{
        height: parent.height
        width: parent.width

        anchors.fill: parent
        model: clientUserData.cards

        spacing: 2

        delegate: Rectangle{
            height: 100
            width: parent.width*0.8
            radius: 10
            anchors{
                horizontalCenter: parent.horizontalCenter
            }
            color: "gray"

            Rectangle {
                visible: !is_default
                Text{
                    text: "🗑"
                    color: "white"
                    font.pointSize: Math.min(parent.width, parent.height)
                    anchors{
                        centerIn: parent
                    }
                }

                height: parent.height/6
                width: parent.height/6

                color: "gray"

                anchors{
                    top: parent.top
                    topMargin: 10
                    right: parent.right
                    rightMargin: 10
                }

                MouseArea{
                    anchors{
                        fill: parent
                    }
                    onClicked: {
                        api.remove_card(clientUserData.token, card_token)
                        clientUserData.update_cards(api.cards(clientUserData.token))
                    }
                }
            }

            Text {
                id: cardName
                text: name
                color: "white"
                font.pointSize: Math.min(parent.width, parent.height)/6

                anchors{
                    top: parent.top
                    topMargin: 10
                    left: parent.left
                    leftMargin: 10
                }
            }

            Text {
                id: cardToken
                text: card_token

                font.pointSize: Math.min(parent.width, parent.height)/12
                color: "white"

                anchors{
                    bottom: parent.bottom
                    bottomMargin: 10
                    left: parent.left
                    leftMargin: 10
                }
            }

            Rectangle{
                id: copyButton
                color: "#00000000"
                width: cardToken.height * 1.5
                height: cardToken.height * 1.5
                anchors{
                    left: cardToken.right
                    leftMargin: width/4
                    verticalCenter: cardToken.verticalCenter
                }


                Text{
                    id: copyLabel
                    text: "🗎"
                    font.pointSize: copyButton.width
                    color: "white"
                    anchors.centerIn: parent

                }

                MouseArea{
                    id: copyMouseArea
                    anchors.fill: parent

                    onClicked: {
                        // TODO: realize copying of token
                    }
                }
            }

            Text {
                id: currencyBalanceCard
                text: balance + " " + currency

                font.pointSize: Math.min(parent.width, parent.height)/8
                color: "white"

                anchors{
                    bottom: parent.bottom
                    bottomMargin: 10
                    right: parent.right
                    rightMargin: 10
                }
            }
        }

    }
}

/*##^##
Designer {
    D{i:0;autoSize:true;formeditorZoom:0.75;height:480;width:640}
}
##^##*/
