"""create level table

Revision ID: 20464bd448b3
Revises: a729d47a048b
Create Date: 2022-05-26 19:12:50.663697

"""
from alembic import op
import sqlalchemy as sa


# revision identifiers, used by Alembic.
revision = '20464bd448b3'
down_revision = 'a729d47a048b'
branch_labels = None
depends_on = None


def upgrade():
    op.create_table(
    'level',
    sa.Column('id', sa.Integer, primary_key=True),
    sa.Column('authorId', sa.String, sa.ForeignKey("account.id"), nullable=False),
    sa.Column('data', sa.String, nullable=False))


def downgrade():
    op.drop_table('level')

