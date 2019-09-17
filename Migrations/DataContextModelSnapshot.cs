﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyTelegramBot.Data;

namespace MyTelegramBot.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("MyTelegramBot.Models.Telegram.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CodeLanguage");

                    b.Property<string>("Key");

                    b.Property<string>("Name");

                    b.Property<int>("Parent");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MyTelegramBot.Models.Telegram.Chat", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("MyTelegramBot.Models.Telegram.Message", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ChatId");

                    b.Property<ulong>("Date");

                    b.Property<DateTime?>("DateRead");

                    b.Property<long>("FromId");

                    b.Property<long>("MessageId");

                    b.Property<DateTime>("MessageSent");

                    b.Property<bool>("RecipientDeleted");

                    b.Property<bool>("SenderDeleted");

                    b.Property<string>("Test");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("FromId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("MyTelegramBot.Models.Telegram.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("CodeLanguage");

                    b.Property<string>("Description");

                    b.Property<string>("Key");

                    b.Property<string>("Name");

                    b.Property<double?>("Price");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("MyTelegramBot.Models.Telegram.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsBot");

                    b.Property<string>("LanguageCode");

                    b.Property<DateTime>("LastActive");

                    b.Property<string>("LastName");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MyTelegramBot.Models.Telegram.UserSetting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ChatId");

                    b.Property<string>("Language");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("MyTelegramBot.Models.Telegram.Message", b =>
                {
                    b.HasOne("MyTelegramBot.Models.Telegram.Chat", "Chat")
                        .WithMany("MessagesReceived")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MyTelegramBot.Models.Telegram.User", "From")
                        .WithMany("MessagesSent")
                        .HasForeignKey("FromId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MyTelegramBot.Models.Telegram.Product", b =>
                {
                    b.HasOne("MyTelegramBot.Models.Telegram.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MyTelegramBot.Models.Telegram.UserSetting", b =>
                {
                    b.HasOne("MyTelegramBot.Models.Telegram.Chat", "Chat")
                        .WithMany()
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyTelegramBot.Models.Telegram.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
