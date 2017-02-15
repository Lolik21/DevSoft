/********************************************************************************
** Form generated from reading UI file 'oop_painter.ui'
**
** Created by: Qt User Interface Compiler version 5.7.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_OOP_PAINTER_H
#define UI_OOP_PAINTER_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QLayout>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QToolBar>
#include <QtWidgets/QToolBox>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_OOP_painter
{
public:
    QWidget *centralWidget;
    QToolBox *toolBox;
    QWidget *page;
    QWidget *page_2;
    QMenuBar *menuBar;
    QToolBar *mainToolBar;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *OOP_painter)
    {
        if (OOP_painter->objectName().isEmpty())
            OOP_painter->setObjectName(QStringLiteral("OOP_painter"));
        OOP_painter->resize(466, 351);
        centralWidget = new QWidget(OOP_painter);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        toolBox = new QToolBox(centralWidget);
        toolBox->setObjectName(QStringLiteral("toolBox"));
        toolBox->setGeometry(QRect(300, 70, 69, 123));
        page = new QWidget();
        page->setObjectName(QStringLiteral("page"));
        page->setGeometry(QRect(0, 0, 69, 69));
        toolBox->addItem(page, QStringLiteral("Page 1"));
        page_2 = new QWidget();
        page_2->setObjectName(QStringLiteral("page_2"));
        page_2->setGeometry(QRect(0, 0, 69, 69));
        toolBox->addItem(page_2, QStringLiteral("Page 2"));
        OOP_painter->setCentralWidget(centralWidget);
        menuBar = new QMenuBar(OOP_painter);
        menuBar->setObjectName(QStringLiteral("menuBar"));
        menuBar->setGeometry(QRect(0, 0, 466, 21));
        OOP_painter->setMenuBar(menuBar);
        mainToolBar = new QToolBar(OOP_painter);
        mainToolBar->setObjectName(QStringLiteral("mainToolBar"));
        OOP_painter->addToolBar(Qt::TopToolBarArea, mainToolBar);
        statusBar = new QStatusBar(OOP_painter);
        statusBar->setObjectName(QStringLiteral("statusBar"));
        OOP_painter->setStatusBar(statusBar);

        retranslateUi(OOP_painter);

        toolBox->setCurrentIndex(0);


        QMetaObject::connectSlotsByName(OOP_painter);
    } // setupUi

    void retranslateUi(QMainWindow *OOP_painter)
    {
        OOP_painter->setWindowTitle(QApplication::translate("OOP_painter", "OOP_painter", 0));
        toolBox->setItemText(toolBox->indexOf(page), QApplication::translate("OOP_painter", "Page 1", 0));
        toolBox->setItemText(toolBox->indexOf(page_2), QApplication::translate("OOP_painter", "Page 2", 0));
    } // retranslateUi

};

namespace Ui {
    class OOP_painter: public Ui_OOP_painter {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_OOP_PAINTER_H
