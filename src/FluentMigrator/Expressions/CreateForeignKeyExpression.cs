﻿using System;
using System.Collections.Generic;

namespace FluentMigrator.Expressions
{
	public class CreateForeignKeyExpression : IMigrationExpression
	{
		public string ForeignTable { get; set; }
		public ICollection<string> ForeignColumns { get; set; }
		public string PrimaryTable { get; set; }
		public ICollection<string> PrimaryColumns { get; set; }

		public CreateForeignKeyExpression()
		{
			ForeignColumns = new List<string>();
			PrimaryColumns = new List<string>();
		}

		public void ExecuteWith(IMigrationProcessor processor)
		{
			processor.Process(this);
		}
	}
}