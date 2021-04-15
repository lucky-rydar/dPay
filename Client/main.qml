import QtQuick 2.12
import QtQuick.Controls 2.0
import QtQuick.Window 2.12

Window {
    id: root
    width: 360
    height: 620
    visible: true
    title: qsTr("dPay")

    RootMenu{
        anchors.fill: parent
    }
}
