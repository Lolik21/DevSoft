#include "oop_painter.h"
#include "ui_oop_painter.h"

OOP_painter::OOP_painter(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::OOP_painter)
{
    ui->setupUi(this);
}

OOP_painter::~OOP_painter()
{
    delete ui;
}
