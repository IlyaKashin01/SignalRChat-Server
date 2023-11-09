﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SignalRChat.Data;

#nullable disable

namespace SignalRChat.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SignalRChat.Domain.Entities.GroupChatRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("CreatorId")
                        .HasColumnType("integer")
                        .HasColumnName("creator_id");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("delete_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("group_chat_room", (string)null);
                });

            modelBuilder.Entity("SignalRChat.Domain.Entities.GroupMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("delete_date");

                    b.Property<int>("FromUserId")
                        .HasColumnType("integer")
                        .HasColumnName("from_person_id");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer")
                        .HasColumnName("group_id");

                    b.Property<bool>("IsCheck")
                        .HasColumnType("boolean")
                        .HasColumnName("is_check");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("sent_at");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("group_message", (string)null);
                });

            modelBuilder.Entity("SignalRChat.Domain.Entities.MessageJoinToGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("delete_date");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer")
                        .HasColumnName("group_id");

                    b.Property<int>("MessageId")
                        .HasColumnType("integer")
                        .HasColumnName("message_id");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("MessageId");

                    b.ToTable("message_join_to_group", (string)null);
                });

            modelBuilder.Entity("SignalRChat.Domain.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ConnectionId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("delete_date");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("person", (string)null);
                });

            modelBuilder.Entity("SignalRChat.Domain.Entities.PersonJoinToGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("delete_date");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer")
                        .HasColumnName("group_id");

                    b.Property<int>("PersonId")
                        .HasColumnType("integer")
                        .HasColumnName("person_id");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("PersonId");

                    b.ToTable("person_join_to_group", (string)null);
                });

            modelBuilder.Entity("SignalRChat.Domain.Entities.PersonalMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("delete_date");

                    b.Property<bool>("IsCheck")
                        .HasColumnType("boolean")
                        .HasColumnName("is_check");

                    b.Property<int?>("PersonId")
                        .HasColumnType("integer");

                    b.Property<int?>("PersonId1")
                        .HasColumnType("integer");

                    b.Property<int>("RecipientId")
                        .HasColumnType("integer")
                        .HasColumnName("recipient_id");

                    b.Property<int>("SenderId")
                        .HasColumnType("integer")
                        .HasColumnName("sender_id");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("sent_at");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("PersonId1");

                    b.ToTable("personal_message", (string)null);
                });

            modelBuilder.Entity("SignalRChat.Domain.Entities.MessageJoinToGroup", b =>
                {
                    b.HasOne("SignalRChat.Domain.Entities.GroupChatRoom", "Group")
                        .WithMany("Messages")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SignalRChat.Domain.Entities.GroupMessage", "Message")
                        .WithMany()
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Message");
                });

            modelBuilder.Entity("SignalRChat.Domain.Entities.PersonJoinToGroup", b =>
                {
                    b.HasOne("SignalRChat.Domain.Entities.GroupChatRoom", "Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SignalRChat.Domain.Entities.Person", "Person")
                        .WithMany("Groups")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("SignalRChat.Domain.Entities.PersonalMessage", b =>
                {
                    b.HasOne("SignalRChat.Domain.Entities.Person", null)
                        .WithMany("RecivedMessages")
                        .HasForeignKey("PersonId");

                    b.HasOne("SignalRChat.Domain.Entities.Person", null)
                        .WithMany("SentMessages")
                        .HasForeignKey("PersonId1");
                });

            modelBuilder.Entity("SignalRChat.Domain.Entities.GroupChatRoom", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("SignalRChat.Domain.Entities.Person", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("RecivedMessages");

                    b.Navigation("SentMessages");
                });
#pragma warning restore 612, 618
        }
    }
}