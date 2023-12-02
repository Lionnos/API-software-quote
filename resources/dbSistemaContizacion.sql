create database dbSistemaCotizaciones;

go
use dbSistemaCotizaciones;
go

create table tUser (
    idUser char(36) not null,
    email varchar(320) not null,
    password varchar(50) not null,
    primary key (idUser)
);

create table tProfessional
(
    idProfessional char(36) not null,
    name varchar(700) not null,
    monthPay decimal(20,2) not null,
    primary key (idProfessional)
);

create table tProjectType 
(
    idProjectType char(36) not null,
    name varchar(700) not null,
    primary key (idProjectType)
);

create table tProjectTypeMechanism
(
    idProjectTypeMechanism char(36) not null,
    idProjectType char(36) not null,
    name varchar(700) not null,
    developerMonthsQuantity float not null,

    primary key (idProjectTypeMechanism),
    foreign key (idProjectType) references tprojectType (idProjectType) on delete cascade on update cascade
);

create table tAssignProject 
(
    idAssignProject char(36) not null,
    idProjectTypeMechanism char(36) not null,
    idProfessional char(36) not null,
    professionalMonthQuantity int not null,
    addProfessional bit not null,
    addProfessionalReducedMonth float not null,

    primary key (idAssignProject),
    foreign key (idProjectTypeMechanism) references tProjectTypeMechanism ( idProjectTypeMechanism) on delete cascade on update cascade,
    foreign key (idProfessional) references tProfessional (idProfessional) on delete cascade on update cascade
);


insert into tUser values('702156c0-8536-4cd6-ada2-b609a45cebdd','Lionos@gmail.com','1001admin');

select * from tUser;
select * from tProfessional;
select * from tProjectType;
select * from tProjectTypeMechanism;
select * from tAssignProject;