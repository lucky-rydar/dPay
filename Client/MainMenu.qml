import QtQuick 2.0
import QtQuick.Controls 2.12
import QtQuick.Layouts 1.3

import cpp.modules 2.0

Page {
    id: mainMenuRoot

    property int baseSideSize: width/5

    /*SwipeView{
        id: mainMenuSwipeView
        height: parent.height - sendMoneyBtn.height
        width: parent.width
        anchors{
            top: parent.top
            topMargin: 0
            left: parent.left
            leftMargin: 0
        }

        SendMoney{
            id: sendMoneyMenu
        }

        Settings{
            id: settingsMenu
        }

        Profile{
            id: profileMenu
        }

        CardsList{
            id: cardListMenu
        }

        CreateCard{
            id: addCardMenu
        }
    }*/

    header: ToolBar {
        contentHeight: toolButton.implicitHeight

        ToolButton {
            id: menuListButton
            text: mainMenuStackView.depth > 1 ? "◀" : "︙"
            font.pixelSize: Qt.application.font.pixelSize * 1.6
            onClicked: {
                if (mainMenuStackView.depth > 1) {
                    mainMenuStackView.pop()
                    clientUserData.update_cards(api.cards(clientUserData.token))
                } else {
                    drawer.open()
                }
            }
        }

        ToolButton {
            id: toolButton
            text: "🗘"
            font.pixelSize: Qt.application.font.pixelSize * 1.6
            anchors.right: parent.right

            onClicked: {
                //here update cards
                clientUserData.update_cards(api.cards(clientUserData.token))
            }
        }

        Label {
            text: mainMenuStackView.currentItem.title
            anchors.centerIn: parent
        }
    }

    Drawer {
        id: drawer
        width: mainMenuRoot.width * 0.66
        height: mainMenuRoot.height

        Column {
            anchors.fill: parent

            ItemDelegate {
                text: qsTr("Profile")
                width: parent.width
                onClicked: {
                    mainMenuStackView.push("Profile.qml")
                    drawer.close()
                }
            }
            ItemDelegate {
                text: qsTr("Create card")
                width: parent.width
                onClicked: {
                    mainMenuStackView.push("CreateCard.qml")
                    drawer.close()
                }
            }
            ItemDelegate {
                text: qsTr("Send money")
                width: parent.width
                onClicked: {
                    mainMenuStackView.push("SendMoney.qml")
                    drawer.close()
                }
            }
            ItemDelegate {
                text: qsTr("Settings")
                width: parent.width
                onClicked: {
                    mainMenuStackView.push("Settings.qml")
                    drawer.close()
                }
            }
            ItemDelegate {
                text: qsTr("Quit")
                width: parent.width
                onClicked: {
                    // do quit from acc
                    drawer.close()
                }
            }
        }
    }

    StackView {
        id: mainMenuStackView
        initialItem: "CardsList.qml"
        anchors.fill: parent
    }




}

/*##^##
Designer {
    D{i:0;autoSize:true;formeditorZoom:0.6600000262260437;height:480;width:640}
}
##^##*/
