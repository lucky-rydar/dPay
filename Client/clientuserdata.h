#ifndef CLIENTUSERDATA_H
#define CLIENTUSERDATA_H

#include <QObject>

class ClientUserData : public QObject
{
    Q_OBJECT

    Q_PROPERTY(QString token MEMBER token)
    Q_PROPERTY(QString username MEMBER username)
    Q_PROPERTY(QString phone MEMBER phone)
    Q_PROPERTY(QString email MEMBER email)
public:
    explicit ClientUserData(QObject *parent = nullptr);

    QString token;
    QString username;
    QString phone;
    QString email;

    // add later list of cards, transactions and other info
signals:

};

#endif // CLIENTUSERDATA_H
