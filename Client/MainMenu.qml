import QtQuick 2.0
import QtQuick.Controls 2.12
import QtQuick.Layouts 1.3

import cpp.modules 2.0

Page {
    id: mainMenuRoot

    property int baseSideSize: width/5

    Button{
        id: sendMoneyBtn
        text: "send money"

        height: mainMenuRoot.baseSideSize
        width: mainMenuRoot.baseSideSize
        anchors.left: parent.left
        anchors.bottom: parent.bottom
        anchors.leftMargin: 0
        anchors.bottomMargin: 0

        onClicked: {
            mainMenuSwipeView.currentIndex = 0
        }
    }
    Button{
        id: settingsBtn
        text: "settings"

        height: mainMenuRoot.baseSideSize
        width: mainMenuRoot.baseSideSize
        anchors.left: sendMoneyBtn.right
        anchors.leftMargin: 0
        anchors.bottom: parent.bottom
        anchors.bottomMargin: 0

        onClicked: {
            mainMenuSwipeView.currentIndex = 1
        }
    }
    Button{
        id: profileBtn
        text: "profile"

        height: mainMenuRoot.baseSideSize
        width: mainMenuRoot.baseSideSize
        anchors.bottom: parent.bottom
        anchors.bottomMargin: 0
        anchors.left: settingsBtn.right
        anchors.leftMargin: 0

        onClicked: {
            mainMenuSwipeView.currentIndex = 2
        }
    }
    Button{
        id: cardsListBtn
        text: "cards"

        height: mainMenuRoot.baseSideSize
        width: mainMenuRoot.baseSideSize
        anchors.bottom: parent.bottom
        anchors.bottomMargin: 0
        anchors.left: profileBtn.right
        anchors.leftMargin: 0

        onClicked: {
            mainMenuSwipeView.currentIndex = 3
        }
    }

    Button{
        id: addCardBtn
        text: "add card"

        height: mainMenuRoot.baseSideSize
        width: mainMenuRoot.baseSideSize
        anchors.bottom: parent.bottom
        anchors.bottomMargin: 0
        anchors.left: cardsListBtn.right
        anchors.leftMargin: 0

        onClicked: {
            mainMenuSwipeView.currentIndex = 4
        }
    }

    SwipeView{
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

        }

        Settings{

        }

        Profile{

        }

        CardsList{

        }

        AddCard{

        }
    }
}

/*##^##
Designer {
    D{i:0;autoSize:true;formeditorZoom:0.6600000262260437;height:480;width:640}
}
##^##*/
