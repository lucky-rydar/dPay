import QtQuick 2.0
import QtQuick.Controls 2.0

Page {

    ListView{
        id: donationsView
        model: clientUserData.donations

        anchors{
            fill: parent
        }

        spacing: 2

        delegate: Rectangle{
            width: parent.width*0.8
            height: 100
            color: "gray"
            radius: 10

            anchors{
                horizontalCenter: parent.horizontalCenter
            }

            Text{
                id: titleLabel
                text: "title: " + title
                color: "white"

                anchors{
                    top: parent.top
                    topMargin: 10
                    left: parent.left
                    leftMargin: 10
                }

                font{
                    pointSize: 12
                }
            }

            Text{
                id: descriptionLabel
                color: "white"

                text: "description: " + (description.length > 10 ? description.substring(0, 9) + "..." : description)

                anchors{
                    bottom: parent.bottom
                    bottomMargin: 10
                    left: parent.left
                    leftMargin: 10
                }

                font{
                    pointSize: 10
                }
            }

            Text{
                id: cardReceiverLabel
                color: "white"
                text: "Card receiver: " + card_receiver
                anchors{
                    top: parent.top
                    topMargin: 10
                    right: parent.right
                    rightMargin: 10
                }
                font{
                    pointSize: 9
                }
            }

            Text{
                id: donationTokenLabel
                color: "white"
                text: "Donation token: " + donation_token
                anchors{
                    verticalCenter: parent.verticalCenter
                    right: parent.right
                    rightMargin: 10
                }
                font{
                    pointSize: 9
                }
            }
        }
    }

    // @disable-check M16
    Component.onCompleted: {
        clientUserData.update_donations(api.donations(clientUserData.token))
    }
}

/*##^##
Designer {
    D{i:0;autoSize:true;height:480;width:640}
}
##^##*/
