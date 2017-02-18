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
#include <QtWidgets/QGraphicsView>
#include <QtWidgets/QGridLayout>
#include <QtWidgets/QGroupBox>
#include <QtWidgets/QHBoxLayout>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenu>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QToolBar>
#include <QtWidgets/QToolBox>
#include <QtWidgets/QToolButton>
#include <QtWidgets/QVBoxLayout>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_OOP_painter
{
public:
    QWidget *centralWidget;
    QHBoxLayout *horizontalLayout;
    QGridLayout *gridLayout;
    QGraphicsView *graphicsView;
    QVBoxLayout *verticalLayout;
    QToolBox *toolBox;
    QWidget *page;
    QWidget *widget;
    QGridLayout *gridLayout_3;
    QGridLayout *gridLayout_2;
    QToolButton *toolButton_2;
    QToolButton *toolButton_4;
    QToolButton *toolButton_5;
    QToolButton *toolButton_3;
    QToolButton *toolButton;
    QWidget *page_2;
    QGroupBox *groupBox;
    QMenuBar *menuBar;
    QMenu *menu;
    QMenu *menu_2;
    QMenu *menu_3;
    QMenu *menu_4;
    QToolBar *mainToolBar;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *OOP_painter)
    {
        if (OOP_painter->objectName().isEmpty())
            OOP_painter->setObjectName(QStringLiteral("OOP_painter"));
        OOP_painter->resize(410, 531);
        centralWidget = new QWidget(OOP_painter);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        horizontalLayout = new QHBoxLayout(centralWidget);
        horizontalLayout->setSpacing(6);
        horizontalLayout->setContentsMargins(11, 11, 11, 11);
        horizontalLayout->setObjectName(QStringLiteral("horizontalLayout"));
        gridLayout = new QGridLayout();
        gridLayout->setSpacing(6);
        gridLayout->setObjectName(QStringLiteral("gridLayout"));
        graphicsView = new QGraphicsView(centralWidget);
        graphicsView->setObjectName(QStringLiteral("graphicsView"));

        gridLayout->addWidget(graphicsView, 0, 0, 1, 1);

        verticalLayout = new QVBoxLayout();
        verticalLayout->setSpacing(6);
        verticalLayout->setObjectName(QStringLiteral("verticalLayout"));
        toolBox = new QToolBox(centralWidget);
        toolBox->setObjectName(QStringLiteral("toolBox"));
        QSizePolicy sizePolicy(QSizePolicy::Fixed, QSizePolicy::Preferred);
        sizePolicy.setHorizontalStretch(0);
        sizePolicy.setVerticalStretch(0);
        sizePolicy.setHeightForWidth(toolBox->sizePolicy().hasHeightForWidth());
        toolBox->setSizePolicy(sizePolicy);
        page = new QWidget();
        page->setObjectName(QStringLiteral("page"));
        page->setGeometry(QRect(0, 0, 69, 383));
        widget = new QWidget(page);
        widget->setObjectName(QStringLiteral("widget"));
        widget->setGeometry(QRect(10, 0, 51, 221));
        gridLayout_3 = new QGridLayout(widget);
        gridLayout_3->setSpacing(6);
        gridLayout_3->setContentsMargins(11, 11, 11, 11);
        gridLayout_3->setObjectName(QStringLiteral("gridLayout_3"));
        gridLayout_3->setContentsMargins(0, 0, 0, 0);
        gridLayout_2 = new QGridLayout();
        gridLayout_2->setSpacing(6);
        gridLayout_2->setObjectName(QStringLiteral("gridLayout_2"));
        toolButton_2 = new QToolButton(widget);
        toolButton_2->setObjectName(QStringLiteral("toolButton_2"));
        QSizePolicy sizePolicy1(QSizePolicy::Preferred, QSizePolicy::Preferred);
        sizePolicy1.setHorizontalStretch(0);
        sizePolicy1.setVerticalStretch(0);
        sizePolicy1.setHeightForWidth(toolButton_2->sizePolicy().hasHeightForWidth());
        toolButton_2->setSizePolicy(sizePolicy1);

        gridLayout_2->addWidget(toolButton_2, 0, 0, 1, 1);

        toolButton_4 = new QToolButton(widget);
        toolButton_4->setObjectName(QStringLiteral("toolButton_4"));
        sizePolicy1.setHeightForWidth(toolButton_4->sizePolicy().hasHeightForWidth());
        toolButton_4->setSizePolicy(sizePolicy1);

        gridLayout_2->addWidget(toolButton_4, 1, 0, 1, 1);

        toolButton_5 = new QToolButton(widget);
        toolButton_5->setObjectName(QStringLiteral("toolButton_5"));
        sizePolicy1.setHeightForWidth(toolButton_5->sizePolicy().hasHeightForWidth());
        toolButton_5->setSizePolicy(sizePolicy1);

        gridLayout_2->addWidget(toolButton_5, 2, 0, 1, 1);

        toolButton_3 = new QToolButton(widget);
        toolButton_3->setObjectName(QStringLiteral("toolButton_3"));
        sizePolicy1.setHeightForWidth(toolButton_3->sizePolicy().hasHeightForWidth());
        toolButton_3->setSizePolicy(sizePolicy1);

        gridLayout_2->addWidget(toolButton_3, 3, 0, 1, 1);

        toolButton = new QToolButton(widget);
        toolButton->setObjectName(QStringLiteral("toolButton"));
        sizePolicy1.setHeightForWidth(toolButton->sizePolicy().hasHeightForWidth());
        toolButton->setSizePolicy(sizePolicy1);
        toolButton->setBaseSize(QSize(0, 0));

        gridLayout_2->addWidget(toolButton, 4, 0, 1, 1);


        gridLayout_3->addLayout(gridLayout_2, 0, 0, 1, 1);

        toolBox->addItem(page, QStringLiteral("Page 1"));
        page_2 = new QWidget();
        page_2->setObjectName(QStringLiteral("page_2"));
        page_2->setGeometry(QRect(0, 0, 69, 383));
        toolBox->addItem(page_2, QStringLiteral("Page 2"));

        verticalLayout->addWidget(toolBox);

        groupBox = new QGroupBox(centralWidget);
        groupBox->setObjectName(QStringLiteral("groupBox"));

        verticalLayout->addWidget(groupBox);


        gridLayout->addLayout(verticalLayout, 0, 1, 1, 1);


        horizontalLayout->addLayout(gridLayout);

        OOP_painter->setCentralWidget(centralWidget);
        menuBar = new QMenuBar(OOP_painter);
        menuBar->setObjectName(QStringLiteral("menuBar"));
        menuBar->setGeometry(QRect(0, 0, 410, 21));
        menu = new QMenu(menuBar);
        menu->setObjectName(QStringLiteral("menu"));
        menu_2 = new QMenu(menuBar);
        menu_2->setObjectName(QStringLiteral("menu_2"));
        menu_3 = new QMenu(menuBar);
        menu_3->setObjectName(QStringLiteral("menu_3"));
        menu_4 = new QMenu(menuBar);
        menu_4->setObjectName(QStringLiteral("menu_4"));
        OOP_painter->setMenuBar(menuBar);
        mainToolBar = new QToolBar(OOP_painter);
        mainToolBar->setObjectName(QStringLiteral("mainToolBar"));
        OOP_painter->addToolBar(Qt::TopToolBarArea, mainToolBar);
        statusBar = new QStatusBar(OOP_painter);
        statusBar->setObjectName(QStringLiteral("statusBar"));
        OOP_painter->setStatusBar(statusBar);

        menuBar->addAction(menu->menuAction());
        menuBar->addAction(menu_2->menuAction());
        menuBar->addAction(menu_3->menuAction());
        menuBar->addAction(menu_4->menuAction());

        retranslateUi(OOP_painter);

        toolBox->setCurrentIndex(0);


        QMetaObject::connectSlotsByName(OOP_painter);
    } // setupUi

    void retranslateUi(QMainWindow *OOP_painter)
    {
        OOP_painter->setWindowTitle(QApplication::translate("OOP_painter", "OOP_painter", 0));
        toolButton_2->setText(QApplication::translate("OOP_painter", "...", 0));
        toolButton_4->setText(QApplication::translate("OOP_painter", "...", 0));
        toolButton_5->setText(QApplication::translate("OOP_painter", "...", 0));
        toolButton_3->setText(QApplication::translate("OOP_painter", "...", 0));
        toolButton->setText(QApplication::translate("OOP_painter", "...", 0));
        toolBox->setItemText(toolBox->indexOf(page), QApplication::translate("OOP_painter", "Page 1", 0));
        toolBox->setItemText(toolBox->indexOf(page_2), QApplication::translate("OOP_painter", "Page 2", 0));
        groupBox->setTitle(QApplication::translate("OOP_painter", "GroupBox", 0));
        menu->setTitle(QApplication::translate("OOP_painter", "\320\244\320\260\320\271\320\273", 0));
        menu_2->setTitle(QApplication::translate("OOP_painter", "\320\240\320\270\321\201\320\276\320\262\320\260\320\275\320\270\320\265", 0));
        menu_3->setTitle(QApplication::translate("OOP_painter", "\320\246\320\262\320\265\321\202", 0));
        menu_4->setTitle(QApplication::translate("OOP_painter", "\320\237\320\276\320\274\320\276\321\211\321\214", 0));
    } // retranslateUi

};

namespace Ui {
    class OOP_painter: public Ui_OOP_painter {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_OOP_PAINTER_H
