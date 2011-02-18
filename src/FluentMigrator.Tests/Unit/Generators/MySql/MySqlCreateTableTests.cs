﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FluentMigrator.Runner.Generators.MySql;
using NUnit.Should;

namespace FluentMigrator.Tests.Unit.Generators.MySql
{
    public class MySqlCreateTableTests : BaseTableCreateTests
    {
        protected MySqlGenerator generator;

        [SetUp]
        public void Setup()
        {
            generator = new MySqlGenerator();
        }

        [Test]
        public override void CanCreateTable()
        {
            var expression = GeneratorTestHelper.GetCreateTableExpression();
            var sql = generator.Generate(expression);
            sql.ShouldBe("CREATE TABLE `TestTable1` (`TestColumn1` VARCHAR(255) NOT NULL, `TestColumn2` INTEGER NOT NULL) ENGINE = INNODB");
  
        }

        [Test]
        public override void CanCreateTableWithCustomColumnType()
        {
            var expression = GeneratorTestHelper.GetCreateTableExpression();
            expression.Columns[0].IsPrimaryKey = true;
            expression.Columns[1].Type = null;
            expression.Columns[1].CustomType = "[timestamp]";
            var sql = generator.Generate(expression);
            sql.ShouldBe(
                "CREATE TABLE `TestTable1` (`TestColumn1` VARCHAR(255) NOT NULL , PRIMARY KEY (`TestColumn1`), `TestColumn2` [timestamp] NOT NULL) ENGINE = INNODB");

        }

        [Test]
        public override void CanCreateTableWithPrimaryKey()
        {
            var expression = GeneratorTestHelper.GetCreateTableWithPrimaryKeyExpression();

            var sql = generator.Generate(expression);
            sql.ShouldBe(
                "CREATE TABLE `TestTable1` (`TestColumn1` VARCHAR(255) NOT NULL , PRIMARY KEY (`TestColumn1`), `TestColumn2` INTEGER NOT NULL) ENGINE = INNODB");

        }

        [Test]
        public override void CanCreateTableWithIdentity()
        {
            var expression = GeneratorTestHelper.GetCreateTableWithGetAutoIncrementExpression();

            var sql = generator.Generate(expression);
            sql.ShouldBe(
                "CREATE TABLE `TestTable1` (`TestColumn1` VARCHAR(255) NOT NULL AUTO_INCREMENT, `TestColumn2` INTEGER NOT NULL) ENGINE = INNODB");
 
        }

        [Test]
        public override void CanCreateTableWithNullField()
        {
            var expression = GeneratorTestHelper.GetCreateTableExpression();
            expression.Columns[0].IsNullable = true;
            var sql = generator.Generate(expression);
            sql.ShouldBe(
                "CREATE TABLE `TestTable1` (`TestColumn1` VARCHAR(255), `TestColumn2` INTEGER NOT NULL) ENGINE = INNODB");
        }

        [Test]
        public override void CanCreateTableWithDefaultValue()
        {
            var expression = GeneratorTestHelper.GetCreateTableExpression();
            expression.Columns[0].DefaultValue = "Default";
            expression.Columns[1].DefaultValue = 0;
            var sql = generator.Generate(expression);
            sql.ShouldBe(
                "CREATE TABLE `TestTable1` (`TestColumn1` VARCHAR(255) NOT NULL DEFAULT 'Default', `TestColumn2` INTEGER NOT NULL DEFAULT 0) ENGINE = INNODB");
  
        }

        [Test]
        public override void CanCreateTableWithDefaultValueExplicitlySetToNull()
        {
            throw new NotImplementedException();
        }

        [Test]
        public override void CanCreateIndex()
        {
            var expression = GeneratorTestHelper.GetCreateIndexExpression();
                   var sql = generator.Generate(expression);
                   sql.ShouldBe("CREATE UNIQUE INDEX IX_TEST ON `TestTable1` (`TestColumn1` ASC)");

        }

        [Test]
        public override void CanCreateMultiColumnIndex()
        {
            var expression = GeneratorTestHelper.GetMultiColumnCreateIndexExpression();
          
            var sql = generator.Generate(expression);
            sql.ShouldBe("CREATE UNIQUE INDEX IX_TEST ON `TestTable1` (`TestColumn1` ASC,`TestColumn1` DESC)");

        }

        [Test]
        public override void CanCreateTableWithMultipartKey()
        {
            throw new NotImplementedException();
        }
    }
}
